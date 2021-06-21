using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Answers;
using EForms.API.Infrastructure.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EForms.API.Core.Services
{
    public class AnswerService : IAnswerService
    {

        /// <summary>
        /// 1 - Create a new FormAnswer Instance
        /// 2 - Create a new List of answers List<Answer>
        /// 3 - Create a new List of errors
        /// 3 - Loop through the recieved answers
        ///                        A - Get each answer's question from the received form
        ///                        b - Apply the question's restriction if any
        ///                             I - IF the answer violated the given restriction
        ///                                 i - Add the question' Id along with the error message whether it is default in the system or customized by the user
        ///                                 ii - Add the wrong answer in the List<Answer> to be returned to the User
        ///                             II - IF the answer is acceptable
        ///                                 i - Add the answer to the List<Answer>
        /// 4 - IF the List of ErrorMessages is empty (ADD FormAnswers TO DB AND SHOULD RETURN Http/200 )
        ///     A - Add the user's id into the FormAnswer instance
        ///     B - Add the List<Answer> into the FormAnswer instance
        ///     C - Update the FormAnswer list in the refrenced FORM.
        /// 5 - IF the List of ErrorMessages is not empty(DONT ADD FormAnswers TO DB AND SHOULD RETURN Http/406)
        /// 6 - Return the List<ErrorMessages>
        /// </summary>
        /// <param name="form"></param>
        /// <param name="formAnswers"></param>
        /// <returns></returns>
        public FormAnswersToReturnDto ValidateFormAnswers(Form form, FormAnswersToInsertDto formAnswers)
        {
            try
            {
                FormAnswersToReturnDto formAnswersToReturn = new FormAnswersToReturnDto();
                formAnswersToReturn.UserId = formAnswers.UserId;

                foreach (AnswerToInsertDto answerDto in formAnswers.Answers)
                {
                    Question question = getQuestionFromDocument(form, answerDto.QuestionId);
                    AnswerToReturnDto answerToReturn = createAnswer(answerDto.QuestionId, answerDto.Answer);

                    bool isValid = false;

                    if (question.Restriction != null && !isValid)
                    {
                        if (question.Type == QuestionType.CheckBox)
                        {
                            List<string> userAnswers = answerDto.Answer.Split(',').ToList();
                            isValid = isAnswerValid(question, userAnswers.Count.ToString());
                        }
                        else
                        {
                            isValid = isAnswerValid(question, answerDto.Answer);
                        }

                        formAnswersToReturn.IsValid = isValid;
                        if (!isValid)
                            answerToReturn.Message = question.Restriction.CustomErrorMessage;
                    }

                    formAnswersToReturn.Answers.Add(answerToReturn);
                }

                return formAnswersToReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool isAnswerValid(Question question, string userAnswer)
        {
            return RestrictionsFactory.ApplyRestriction(question.Restriction, userAnswer);
        }
        private AnswerToReturnDto createAnswer(string questionId, string userAnswer)
        {
            try
            {
                return new AnswerToReturnDto
                {
                    QuestionId = questionId,
                    Answer = userAnswer
                };
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new answer for question {questionId}: {ex.Message}", ex);
            }
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
    }
}
