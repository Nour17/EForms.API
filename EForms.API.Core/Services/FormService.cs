using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services
{
    public class FormService : IFormService
    {
        // Create the needed instances in Validation process 
        FormAnswer formAnswerToCreate = new FormAnswer();
        List<Answer> answers = new List<Answer>();
        List<ErrorMessage> formErrors = new List<ErrorMessage>();

        public List<ErrorMessage> ValidateFormAnswers(ref Form form, FormAnswersDto formAnswers)
        {
            /*
             * Answering a form process
             *      1 - Create a new FormAnswer Instance
             *      2 - Create a new List of answers List<Answer>
             *      3 - Create a new List of errors
             *      3 - Loop through the recieved answers
             *          A - Get each answer's question from the received form
             *          b - Apply the question's restriction if any
             *                  I - IF the answer violated the given restriction 
             *                          i - Add the question' Id along with the error message whether it is default in the system or customized by the user
             *                          ii - Add the wrong answer in the List<Answer> to be returned to the User
             *                  II - IF the answer is acceptable
             *                          i - Add the answer to the List<Answer>
             *      4 - IF the List of ErrorMessages is empty ( ADD FormAnswers TO DB AND SHOULD RETURN Http/200 )
             *          A - Add the user's id into the FormAnswer instance
             *          B - Add the List<Answer> into the FormAnswer instance
             *          C - Update the FormAnswer list in the refrenced FORM.
             *      5 - IF the List of ErrorMessages is not empty ( DONT ADD FormAnswers TO DB AND SHOULD RETURN Http/406)
             *      6 - Return the List<ErrorMessages>
             */

            formErrors = validateAllAnswers(form, formAnswers);

            // If the form have a single invalid answer return the formError List to the User
            if (formErrors.Count > 0)
                return formErrors;
            
            // If all given answers is valid
            // Add the user's id to the formAnswer object
            formAnswerToCreate.UserId = formAnswers.UserId;
            // Add the user's answers to the formAnswer object
            formAnswerToCreate.Answers = answers;

            // Empty object to hold the stored sections in the fetchedForm
            var existedFormAnswers = new List<FormAnswer>();

            // Check whether the fethed form is answered before by other users or not
            /*
             * If any answers were already in the fetched form
             * a copy should be done to add to it the newly formAnswers
            */
            if (form.FormAnswers != null)
                existedFormAnswers = form.FormAnswers;

            existedFormAnswers.Add(formAnswerToCreate);
            form.FormAnswers = existedFormAnswers;

            return formErrors;
        }

        private List<ErrorMessage> validateAllAnswers(Form form, FormAnswersDto formAnswers)
        {
            List<ErrorMessage> formErrorMessages = new List<ErrorMessage>();

            // Loop through the revieved answers
            foreach (FormAnswerDto answerDto in formAnswers.Answers)
            {
                Question question = GetQuestionFromDocument(form, answerDto.QuestionId);

                // No Restrictions THEN add the answer to the list
                if (question.Restriction == null)
                {
                    addValidatedAnswer(answerDto.QuestionId, answerDto.UserAnswer);
                }
                // There is a restriction THEN apply the validation sequence  
                else
                {
                    ErrorMessage errorMessage = validateAnswer(question, answerDto.UserAnswer);
                    if (errorMessage != null)
                        formErrorMessages.Add(errorMessage);
                }
            }

            return formErrorMessages;
        }

        // Validate each answer
        private ErrorMessage validateAnswer(Question question, string userAnswer)
        {
            // Create Form errors model 
            ErrorMessage errorMessage = new ErrorMessage();

            // The RestrictionsFactory will Map to the appropriate logic based on the Question Restriction
            bool isAcceptable = RestrictionsFactory.ApplyRestriction(question.Restriction, userAnswer, question.Restriction.RightOperand, question.Restriction.ExtraOperand);

            // The Answer is VALID THEN add the answer to the list
            if (isAcceptable)
            {
                addValidatedAnswer(question.InternalId, userAnswer);
            }
            // The Answer is INVALID THEN make an error message and add it to the List<ErrorMessages>
            else
            {
                // Populate the errorMessage
                errorMessage.QuestionId = question.InternalId;
                errorMessage.Message = question.Restriction.CustomErrorMessage;
                return errorMessage;
            }

            return null;
        }

        private void addValidatedAnswer(string questionId, string userAnswer)
        {
            Answer answer = new Answer
            {
                QuestionId = questionId,
                UserAnswer = userAnswer
            };

            answers.Add(answer);
        }

        private Question GetQuestionFromDocument(Form form, string questionId)
        {
            // If the question found in Form questions
            Question question = form.Questions.Find(x => x.InternalId == questionId);

            // If the question is in a certain section
            if (question == null)
            {
                // loop over all the sections until the question is found
                foreach (Section section in form.Sections)
                {
                    question = section.Questions.Find(x => x.InternalId == questionId);
                    if (question != null)
                        break;
                }
            }

            return question;
        }
    }
}
