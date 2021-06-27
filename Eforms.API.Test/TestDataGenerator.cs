using System;
using System.Collections.Generic;
using System.Text;
using EForms.API.Core.Models;
using EForms.API.Infrastructure.Enums;

namespace EForms.API.Test
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetCorrectFormFromDataGeneraor()
        {
            yield return new object[] {
                new FormCore {
                    Header = "Form One",
                    Description = "Form One Descritpion",
                    ColumnRepresentation = 1,
                    Position = 0,
                    Sections = new List<SectionCore>() {
                        new SectionCore
                        {
                            InternalId = "1",
                            Header = "Text Based Section",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 1,
                            Questions = new List<QuestionCore>()
                            {
                                new QuestionCore
                                {
                                    InternalId = "2",
                                    Header="Would you rather be stuck on a broken ski lift or a broken elevator?",
                                    Description="Question Without Condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText
                                }
                            }
                        },
                        new SectionCore
                        {
                            InternalId = "3",
                            Header = "Text Based Section",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 2,
                            Questions = new List<QuestionCore>()
                            {
                                new QuestionCore
                                {
                                    InternalId = "4",
                                    Header="Would you rather be stuck on a broken ski lift or a broken elevator?",
                                    Description="Question Without Condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText
                                }
                            }
                        },
                    },
                    Questions = new List<QuestionCore>()
                        {
                            new QuestionCore
                            {
                                InternalId = "5",
                                Header="What is one of your favorite smells?",
                                Description="Question With Max Length Condition",
                                Position= 3,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.NormalText,
                                Restriction = new RestrictionCore() {
                                    Condition = RestrictionType.MaxStringLength,
                                    RightOperand = "5",                                        
                                    CustomErrorMessage = "Do not exceed 5 characters"
                                }
                            }
                        }
                },
                new FormAnswersCore {
                    UserId = "60d0dd4be82549e5ee4b3664",
                    Answers = new List<AnswerCore>(){
                        new AnswerCore {
                            QuestionId = "2",
                            UserAnswer = "Answer"
                        },
                        new AnswerCore {
                            QuestionId = "4",
                            UserAnswer = "Answer"
                        },
                        new AnswerCore {
                            QuestionId = "5",
                            UserAnswer = "Answe"
                        },
                    }
                },
            };
        }
    }
}
