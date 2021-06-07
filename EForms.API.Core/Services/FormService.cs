using Contracts;
using ErrorHandlingService;
using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Dtos.Section;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using EForms.API.Repository.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EForms.API.Core.Services
{
    public class FormService : IFormService
    {
        private readonly ILoggerManager _logger;
        private readonly IFormRepository _formRepository;

        public FormService(ILoggerManager logger,
                           IFormRepository formRepository)
        {
            _logger = logger;
            _formRepository = formRepository;
        }

        public async Task<Form> AddForm(Form formToAdd)
        {
            // Add the form to the DB
            var addedForm = await _formRepository.AddForm<Form>(formToAdd);

            if (addedForm == null)
                throw new Exception("Adding form to DB failed!!");

            return addedForm;
        }

        public async Task<Form> GetForm(string id)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                throw new Exception("This form doesn't exist!!");

            return fetchedForm;
        }

        public async Task<List<Form>> GetForms()
        {
            var fetchedForms = await _formRepository.GetForms<Form>();

            // Check the form existence in the DB
            if (fetchedForms == null)
                throw new Exception("No forms exist!!");

            return fetchedForms;
        }

        // Check availability of at least one question on the entire form either in questions or a specific section
        public bool IsReceivedFormValid(FormToInsertDto formToInsert)
        {
            _logger.LogInfo("Checking validity of Incoming Form");
            bool response = false;

            response = containQuestions<FormToInsertDto>(formToInsert);

            if (formToInsert.Sections != null)
            {
                foreach (SectionToInsertDto sectionToInsert in formToInsert.Sections)
                {
                    response = containQuestions<SectionToInsertDto>(sectionToInsert);
                }
            }

            if (!response)
                _logger.LogError("Incoming Form is invalid");

            _logger.LogInfo("Incoming Form is valid");

            return response;
        }

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

            AnsweredForm formAnswerToCreate = new AnsweredForm();
            List<Answer> answers = new List<Answer>();
            List<ErrorMessage> errorMessages = new List<ErrorMessage>();

            _logger.LogInfo("Validate answered form");
            validateAllAnswers(form, formAnswers, ref answers, ref errorMessages);

            // If the form have a single invalid answer return the formError List to the User
            if (errorMessages.Count > 0)
                throw new Exception("No forms exist!!");

            // If all given answers is valid
            // Add the user's id to the formAnswer object
            formAnswerToCreate.UserId = formAnswers.UserId;
            // Add the user's answers to the formAnswer object
            formAnswerToCreate.Answers = answers;
            // Add the formAnswerObject to the existed answers in the form
            form.FormAnswers.Add(formAnswerToCreate);

            return errorMessages;
        }

        private void validateAllAnswers(Form form, FormAnswersDto formAnswers, ref List<Answer> answers, ref List<ErrorMessage> errorMessages)
        {
            // Loop through the revieved answers
            foreach (FormAnswerDto answerDto in formAnswers.Answers)
            {
                Question question = getQuestionFromDocument(form, answerDto.QuestionId);

                // No Restrictions THEN add the answer to the list
                if (question.Restriction == null)
                {
                    answers.Add(addValidatedAnswer(answerDto.QuestionId, answerDto.UserAnswer));
                }
                // There is a restriction THEN apply the validation sequence  
                else
                {
                    ErrorMessage errorMessage = validateAnswer(question, answerDto.UserAnswer);
                    if (errorMessage != null)
                        errorMessages.Add(errorMessage);
                }
            }
        }

        // Validate each answer
        private ErrorMessage validateAnswer(Question question, string userAnswer)
        {
            // Create Form errors model 
            ErrorMessage errorMessage = new ErrorMessage();

            // The RestrictionsFactory will Map to the appropriate logic based on the Question Restriction
            bool isAcceptable = RestrictionsFactory.ApplyRestriction(question.Restriction, userAnswer);

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

        private Answer addValidatedAnswer(string questionId, string userAnswer)
        {
            Answer answer = new Answer
            {
                QuestionId = questionId,
                UserAnswer = userAnswer
            };

            return answer;
        }

        private Question getQuestionFromDocument(Form form, string questionId)
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

        private bool containQuestions<T>(IContainerToCreateDto questionContainer)
        {
            if (questionContainer.Questions != null)
            {
                if (questionContainer.Questions.Count != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
