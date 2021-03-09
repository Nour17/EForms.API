using EForms.API.Dtos.Question;
using EForms.API.Models;
using EForms.API.Models.Interfaces;
using EForms.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services
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

        public bool restrictAnswer(Question question, string userAnswer)
        {
            Restriction restriction = question.Restriction;

            switch(restriction.Condition)
            {
                // Normal Text Restriction Check
                case RestrictionType.MaxStringLength:
                    return checkMaxStringLength(userAnswer, int.Parse(restriction.RightOperand));
                case RestrictionType.MinStringLength:
                    return checkMinStringLength(userAnswer, int.Parse(restriction.RightOperand));
                case RestrictionType.StringContains:
                    return checkStringContains(userAnswer, restriction.RightOperand);
                case RestrictionType.StringDontContains:
                    return !checkStringContains(userAnswer, restriction.RightOperand);
                // Number Restriction Check
                // I rather remove this condition
                case RestrictionType.IsNumber:
                    return checkNumberType(userAnswer, (NumberType) int.Parse(restriction.RightOperand));
            }

            return false;
        }

        private bool checkMaxStringLength(string userAnswer, int maxLength)
        {
            if (userAnswer.Length < maxLength)
                return true;
            return false;
        }
        private bool checkMinStringLength(string userAnswer, int minLength)
        {
            if (userAnswer.Length > minLength)
                return true;
            return false;
        }
        private bool checkStringContains(string userAnswer, string searchText)
        {
            if (userAnswer.Contains(searchText))
                return true;
            return false;
        }
        private bool checkNumberType(string userAnswer, NumberType numType)
        {
            if (numType == NumberType.Integer)
            {
                try
                {
                    int x = int.Parse(userAnswer);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            } else if (numType == NumberType.Float)
            {
                try
                {
                    float x = float.Parse(userAnswer);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            }

            return false;
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
