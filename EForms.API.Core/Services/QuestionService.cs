using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;
using EForms.API.Core.Services.Interfaces;
using System.Collections.Generic;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Core.Dtos.Question;
using System.Linq;
using EForms.API.Core.Dtos.Restriction;
using EForms.API.Core.Dtos.Option;
using EForms.API.Core.Dtos.Range;
using Contracts;

namespace EForms.API.Core.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ILoggerManager _logger;
        public QuestionService(ILoggerManager logger)
        {
            _logger = logger;
        }
        public Question GetQuestion<T>(T parentElement, string questionId)
        {
            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)parentElement;

            return containerElement.Questions.FirstOrDefault(x => x.InternalId == questionId);
        }
        public Question CreateQuestion(QuestionToInsertDto questionToInsertDto)
        {
            try
            {
                Question createdQuestion = populateQuestionWithBasicInfo(questionToInsertDto);
                createdQuestion = questionAddOns(createdQuestion, questionToInsertDto);

                return createdQuestion;
            }
            catch (System.Exception ex)
            {
                string errorMessage = $"Problem while creating question process \n{ex.Message}";
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    errorMessage += "\n" + innerException.Message;
                    innerException = innerException.InnerException;
                }
                _logger.LogError(errorMessage + "\n" + ex.StackTrace);
                throw new System.Exception(errorMessage);
            }
        }
        private Question populateQuestionWithBasicInfo(QuestionToInsertDto questionToInsertDto)
        {
            _logger.LogInfo($"Populating question with basic data");

            try
            {
                return new Question
                {
                    Header = questionToInsertDto.Header,
                    Description = questionToInsertDto.Description,
                    IsRequired = questionToInsertDto.IsRequired,
                    Position = questionToInsertDto.Position,
                    Genre = (QuestionGenre)questionToInsertDto.Genre
                };
            } catch (System.Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new question: {ex.Message}", ex);
            }
        }
        private Question questionAddOns(Question question, QuestionToInsertDto questionToInsertDto)
        {
            try
            {
                QuestionType questionType = (QuestionType)questionToInsertDto.Type;

                if (questionToInsertDto.Restriction != null)
                {
                    _logger.LogInfo("Attempt to initialize new restriction object");
                    Restriction restriction = createRestriction(questionToInsertDto.Restriction);
                    _logger.LogInfo("Adding restriction object into question");
                    question.Restriction = restriction;
                }

                if (questionType == QuestionType.CheckBox ||
                    questionType == QuestionType.RadioButton ||
                    questionType == QuestionType.CheckBox)
                {
                    _logger.LogInfo("Attempt to initialize new option object");
                    Options options = createOptions(questionToInsertDto.Options);
                    _logger.LogInfo("Adding Option object into question");
                    question.Options = options;
                }
                else if (questionType == QuestionType.RangeInput)
                {
                    _logger.LogInfo("Attempt to initialize new range object");
                    Range range = createRange(questionToInsertDto.Range);
                    _logger.LogInfo("Adding Range object into question");
                    question.Range = range;
                }

                return question;
            } catch (System.ArgumentException)
            {
                throw;
            }
        }
        private Restriction createRestriction(RestrictionToAddDto restrictionToAddDto)
        {
            try
            {
                // If there are restrictions object sent then create and populate Restriction Object
                return new Restriction
                {
                    Condition = (RestrictionType)restrictionToAddDto.Condition,
                    RightOperand = restrictionToAddDto.RightOperand,
                    ExtraOperand = restrictionToAddDto.ExtraOperand,
                    CustomErrorMessage = restrictionToAddDto.CustomErrorMessage
                };
            }
            catch (System.Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new restriction: {ex.Message}", ex);
            }
        }
        private Options createOptions(OptionsToAddDto optionsToAddDto)
        {
            try
            {
                // If the question's type is range, so the required data of the range Object must be available 
                // otherwise throw an EXCEPTION
                if (optionsToAddDto == null)
                    throw new System.ArgumentNullException("Option based question requires the basic data to create one!!");

                return new Options
                {
                    OtherOption = optionsToAddDto.OtherOption,
                    CustomOptions = optionsToAddDto.CustomOptions
                };
            }
            catch (System.Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new options: {ex.Message}", ex);
            }
        }
        private Range createRange(RangeToAddDto rangeToAddDto)
        {
            try
            {
                // If the question's type is range, so the required data of the range Object must be available 
                // otherwise throw an EXCEPTION
                if (rangeToAddDto == null)
                    throw new System.ArgumentNullException("Range based question requires the basic data to create one!!");

                return new Range
                {
                    LeftLabel = rangeToAddDto.LeftLabel,
                    RightLabel = rangeToAddDto.RightLabel,
                    LeftValue = rangeToAddDto.LeftValue,
                    RightValue = rangeToAddDto.RightValue
                };
            }
            catch (System.Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new range: {ex.Message}", ex);
            }
        }
    }
}
