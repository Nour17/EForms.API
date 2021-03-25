using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;
using EForms.API.Core.Services.Interfaces;
using System.Collections.Generic;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Core.Dtos.Question;
using System.Linq;

namespace EForms.API.Core.Services
{
    public class QuestionService : IQuestionService
    {
        public Question GetQuestion<T>(T parentElement, string questionId)
        {
            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)parentElement;

            return containerElement.Questions.FirstOrDefault(x => x.InternalId == questionId);
        }

        public void InsertQuestion<T>(ref T parentElement, QuestionToInsertDto questionToInsertDto)
        {
            Question questionToInsert = populateQuestion(questionToInsertDto);

            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)parentElement;

            // Add the question to the Question list in the parent element
            containerElement.Questions.Add(questionToInsert);

            // Override the sent property with the updated IContainerElement object and cast it back to generic type
            parentElement = (T)containerElement;
        }

        private Question populateQuestion(QuestionToInsertDto questionToInsertDto)
        {
            // Create and populate new question instance
            Question createdQuestion = new Question
            {
                Header = questionToInsertDto.Header,
                Description = questionToInsertDto.Description,
                IsRequired = questionToInsertDto.IsRequired,
                Position = questionToInsertDto.Position,
                Genre = (QuestionGenre)questionToInsertDto.Genre,
                Type = (QuestionType)questionToInsertDto.Type,
                Options = questionToInsertDto.Options,
                Restriction = new Restriction
                {
                    Condition = (RestrictionType)questionToInsertDto.RestrictionCondition,
                    RightOperand = questionToInsertDto.RightOperand,
                    ExtraOperand = questionToInsertDto.ExtraOperand,
                    CustomErrorMessage = questionToInsertDto.CustomErrorMessage
                }
            };

            return createdQuestion;
        }
    }
}
