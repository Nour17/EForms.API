using System.Collections.Generic;
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Enums;
using MongoDB.Driver;

namespace EForms.API.Repository.Data
{
    public class FormSeeder {
        public static void SeedData(IMongoCollection<Form> formCollection) {
            bool formsExist = formCollection.Find(forms => true).Any();
            if(!formsExist) {
                formCollection.InsertManyAsync(GetPreconfiguredForms());
            }
        }

        private static IEnumerable<Form> GetPreconfiguredForms() {
            return new List<Form> {
                new Form {
                    Header = "Form One",
                    Description = "Form One Descritpion",
                    ColumnRepresentation = 1,
                    Position = 0,
                    Sections = new List<Section>() {
                        new Section
                        {
                            Header = "Text Based Section",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 1,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="Would you rather be stuck on a broken ski lift or a broken elevator?",
                                    Description="Question Without Condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question With Max Length Condition",
                                    Position= 2,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.MaxStringLength,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Do not exceed 5 characters"
                                    }
                                },
                                new Question
                                {
                                    Header="In your group of friends, what role do you play?",
                                    Description="Question With Min Length Condition",
                                    Position= 3,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.MinStringLength,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Minimum 5 characters"
                                    }
                                },
                                new Question
                                {
                                    Header="If you had to change your name, what would you change it to?",
                                    Description="Question With Contains Condition",
                                    Position= 3,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.StringContains,
                                        RightOperand = "Nour",
                                        CustomErrorMessage = "Answer should be or have Nour"
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question With Not Contains Condition",
                                    Position= 4,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.StringDontContains,
                                        RightOperand = "Nour",
                                        CustomErrorMessage = "Answer shouldn't be or have Nour"
                                    }
                                }
                                
                            }
                        },
                        new Section
                        {
                            Header = "Number based Question",
                            Description = "Section Two Descritpion",
                            ColumnRepresentation = 1,
                            Position = 2,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with integer type condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.IsNumberInteger,
                                        CustomErrorMessage = "Number should be integer"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with double type condition",
                                    Position= 2,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.IsNumberDouble,
                                        CustomErrorMessage = "Number should be double"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with less than condition",
                                    Position= 3,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsLessThan,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should be less than 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with less than or equal condition",
                                    Position= 4,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsLessThanOrEqual,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should be less than or equal 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with biggger than condition",
                                    Position= 5,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsBiggerThan,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should be biggger than 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with biggger than or equal condition",
                                    Position= 6,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsBiggerThanOrEqual,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should be biggger than or equal 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with equal condition",
                                    Position= 7,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberEqual,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should be equal 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with not equal condition",
                                    Position= 8,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberDoNotEqual,
                                        RightOperand = "5",
                                        CustomErrorMessage = "Number should not be equal 5"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with between condition",
                                    Position= 10,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsBetween,
                                        RightOperand = "5",
                                        ExtraOperand = "10",
                                        CustomErrorMessage = "Number should be between 5 and 10"
                                    }
                                },
                                new Question
                                {
                                    Header="What is one of your favorite smells?",
                                    Description="Question with not between condition",
                                    Position= 11,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Number,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.NumberIsNotBetween,
                                        RightOperand = "5",
                                        ExtraOperand = "10",
                                        CustomErrorMessage = "Number should not be between 5 and 10"
                                    }
                                }
                                
                            }
                        },
                        new Section
                        {
                            Header = "Date based Question",
                            Description = "Section Two Descritpion",
                            ColumnRepresentation = 1,
                            Position = 3,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with after date condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Date,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.DateIsAfter,
                                        RightOperand = "06/23/2021",
                                        CustomErrorMessage = "Date should be after 06/23/2021"
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with equal date condition",
                                    Position= 2,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Date,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.DateIsEqual,
                                        RightOperand = "06/23/2021",
                                        CustomErrorMessage = "Date should be equal 06/23/2021"
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with before date condition",
                                    Position= 3,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Date,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.DateIsBefore,
                                        RightOperand = "06/23/2021",
                                        CustomErrorMessage = "Date should be before 06/23/2021"
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with between date condition",
                                    Position= 4,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Date,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.DateIsBetween,
                                        RightOperand = "06/23/2021",
                                        ExtraOperand = "06/30/2021",
                                        CustomErrorMessage = "Date should be between 06/23/2021 and 06/30/2021" 
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with not between date condition",
                                    Position= 5,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.Date,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.DateIsNotBetween,
                                        RightOperand = "06/23/2021",
                                        ExtraOperand = "06/30/2021",
                                        CustomErrorMessage = "Date should be between 06/23/2021 and 06/30/2021" 
                                    }
                                }
                            }
                        },
                        new Section
                        {
                            Header = "Checkbox based Question",
                            Description = "Section Two Descritpion",
                            ColumnRepresentation = 1,
                            Position = 4,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with At least choosen condition",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.OptionBased,
                                    Type = QuestionType.CheckBox,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.CheckboxAtLeastChecked,
                                        RightOperand = "2",
                                        CustomErrorMessage = "Atleast 2 checkboxes should be choosen"
                                    },
                                    Options = new Options() {
                                        OtherOption = false,
                                        CustomOptions = new List<string>(){"First", "Second", "Third"}
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with exactly choosen condition with other option",
                                    Position= 2,
                                    IsRequired = false,
                                    Genre = QuestionGenre.OptionBased,
                                    Type = QuestionType.CheckBox,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.CheckboxExactlyChecked,
                                        RightOperand = "2",
                                        CustomErrorMessage = "Exactly 2 checkboxes should be choosen"
                                    },
                                    Options = new Options() {
                                        OtherOption = true,
                                        CustomOptions = new List<string>(){"First", "Second", "Third"}
                                    }
                                },
                                new Question
                                {
                                    Header="What was your best birthday?",
                                    Description="Question with At most choosen condition",
                                    Position= 3,
                                    IsRequired = false,
                                    Genre = QuestionGenre.OptionBased,
                                    Type = QuestionType.CheckBox,
                                    Restriction = new Restriction() {
                                        Condition = RestrictionType.CheckboxAtMostChecked,
                                        RightOperand = "2",
                                        CustomErrorMessage = "Atmost 2 checkboxes should be choosen"
                                    },
                                    Options = new Options() {
                                        OtherOption = true,
                                        CustomOptions = new List<string>(){"First", "Second", "Third"}
                                    }
                                },
                            }
                        }
                    },
                    Questions = new List<Question>()
                        {
                            new Question
                            {
                                Header="Email question",
                                Description="Question five in Form Description",
                                Position= 5,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.Email,
                                Restriction = new Restriction() {
                                        Condition = RestrictionType.EmailType,
                                        CustomErrorMessage = "Input shoul be email"
                                }
                            },
                            new Question
                            {
                                Header="Url question",
                                Description="Question six in Form Description",
                                Position= 6,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.URL,
                                Restriction = new Restriction() {
                                        Condition = RestrictionType.URLType,
                                        CustomErrorMessage = "Input shoul be url"
                                }
                            },
                            new Question
                            {
                                Header="Time question",
                                Description="Question seven in Form Description",
                                Position= 7,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.Time
                            }
                            ,
                            new Question
                            {
                                Header="Range question",
                                Description="Question eight in Form Description",
                                Position= 8,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.RangeInput,
                                Range = new Range() {
                                    LeftLabel = "Left",
                                    RightLabel = "Right",
                                    LeftValue = 0,
                                    RightValue = 100,
                                }
                            }
                        }
                }
            };
        }
    }
}