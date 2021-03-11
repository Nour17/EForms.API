using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;
using EForms.API.Core.Services.Interfaces;
using System.Collections.Generic;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Core.Dtos.Question;

namespace EForms.API.Core.Services
{
    public class QuestionService : IQuestionService
    {
        public Question GetQuestion<T>(ref T parentElement, string questionId)
        {
            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)parentElement;

            return containerElement.Questions.Find(x => x.InternalId == questionId);
        }

        public void InsertQuestion<T>(ref T parentElement, QuestionToInsertDto questionToInsertDto)
        {
            Question questionToInsert = populateQuestion(questionToInsertDto);

            var existedQuestions = new List<Question>();
            /*
                * If any questions were already in the fetched form
                * a copy should be done to add to it the newly question
            */

            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)parentElement;

            if (containerElement.Questions != null)
                existedQuestions = containerElement.Questions;

            existedQuestions.Add(questionToInsert);
            // Copy back the updated list of questions
            containerElement.Questions = existedQuestions;

            // Override the sent property with the updated IContainerElement object and cast it back to generic type
            parentElement = (T)containerElement;
        }

        public bool CheckAnswer(Question question, string userAnswer)
        {
            Restriction restriction = question.Restriction;

            var restrictionChecker = RestrictionsFactory.CreateRestriction(restriction);
            var isAcceptable = restrictionChecker.checkRestriction(userAnswer, question.Restriction.RightOperand);

            return isAcceptable;
        }

        private Question populateQuestion(QuestionToInsertDto questionToInsertDto)
        {
            // Create and populate new question instance
            Question createdQuestion = new Question
            {
                Header = questionToInsertDto.Header,
                Description = questionToInsertDto.Description,
                IsRequired = questionToInsertDto.IsRequired,
                Genre = (QuestionGenre)questionToInsertDto.Genre,
                Type = (QuestionType)questionToInsertDto.Type,
                Restriction = new Restriction
                {
                    Condition = (RestrictionType)questionToInsertDto.RestrictionCondition,
                    RightOperand = questionToInsertDto.RightOperand,
                    ExtraOperand = questionToInsertDto.ExtraOperand
                }
            };

            return createdQuestion;
        }
    }
}
