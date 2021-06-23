using System;
using System.Collections.Generic;
using System.Text;
using EForms.API.Core.Models;
using EForms.API.Infrastructure.Enums;

namespace EForms.API.Test.Core
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
                            Header = "Section One",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 1,
                            Questions = new List<QuestionCore>()
                            {
                                new QuestionCore
                                {
                                    Header="Question One in Section One",
                                    Description="Question One in Section One Description",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = QuestionGenre.TextBased,
                                    Type = QuestionType.NormalText
                                }
                            }
                        },
                        new SectionCore
                        {
                            Header = "Section Two",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 2,
                            Questions = new List<QuestionCore>()
                            {
                                new QuestionCore
                                {
                                    Header="Question One in Section Two",
                                    Description="Question One in Section Two Description",
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
                                Header="Question One in Form",
                                Description="Question One in Form Description",
                                Position= 1,
                                IsRequired = false,
                                Genre = QuestionGenre.TextBased,
                                Type = QuestionType.NormalText
                            }
                        }
                }
            };
        }
    }
}
