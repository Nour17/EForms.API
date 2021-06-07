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

        public void InsertQuestion(IContainerElement parentElement, QuestionToInsertDto questionToInsertDto)
        {
            Question questionToInsert = populateQuestion(questionToInsertDto);

            // Add the question to the Question list in the parent element
            parentElement.Questions.Add(questionToInsert);
        }

        private Question populateQuestion(QuestionToInsertDto questionToInsertDto)
        {
            // If there are restrictions object sent then create and populate Restriction Object
            Restriction restriction = null;
            if (questionToInsertDto.Restriction != null)
            {
                restriction = new Restriction
                {
                    Condition = (RestrictionType)questionToInsertDto.Restriction.Condition,
                    RightOperand = questionToInsertDto.Restriction.RightOperand,
                    ExtraOperand = questionToInsertDto.Restriction.ExtraOperand,
                    CustomErrorMessage = questionToInsertDto.Restriction.CustomErrorMessage
                };
            }

            // If there are restrictions object sent then create and populate Restriction Object
            Options options = null;
            if (questionToInsertDto.Options != null)
            {
                options = new Options
                {
                    OtherOption = questionToInsertDto.Options.OtherOption,
                    CustomOptions = questionToInsertDto.Options.CustomOptions
                };
            }

            // If there are restrictions object sent then create and populate Restriction Object
            Range range = null;
            if (questionToInsertDto.Range != null)
            {
                range = new Range
                {
                    LeftLabel = questionToInsertDto.Range.LeftLabel,
                    RightLabel = questionToInsertDto.Range.RightLabel,
                    LeftValue = questionToInsertDto.Range.LeftValue,
                    RightValue = questionToInsertDto.Range.RightValue
                };
            }

            // Create and populate new question instance
            Question createdQuestion = new Question
            {
                Header = questionToInsertDto.Header,
                Description = questionToInsertDto.Description,
                IsRequired = questionToInsertDto.IsRequired,
                Position = questionToInsertDto.Position,
                Genre = (QuestionGenre)questionToInsertDto.Genre,
                Type = (QuestionType)questionToInsertDto.Type,
                Options = options,
                Range = range,
                Restriction = restriction
            };

            return createdQuestion;
        }
    }
}
