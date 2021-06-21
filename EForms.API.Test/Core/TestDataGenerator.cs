using EForms.API.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Test.Core
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetFormFromDataGeneraor()
        {
            yield return new object[] {
                new Form {
                    Header = "Form One",
                    Description = "Form One Descritpion",
                    ColumnRepresentation = 1,
                    Position = 0,
                    UserId = "6047567f585ed3dbd834c588",
                    Sections = new List<Section>() {
                        new Section
                        {
                            Header = "Section One",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 1,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="Question One in Section One",
                                    Description="Question One in Section One Description",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = 1,
                                    Type = 1
                                }
                            }
                        },
                        new Section
                        {
                            Header = "Section Two",
                            Description = "Section One Descritpion",
                            ColumnRepresentation = 1,
                            Position = 2,
                            Questions = new List<Question>()
                            {
                                new Question
                                {
                                    Header="Question One in Section Two",
                                    Description="Question One in Section Two Description",
                                    Position= 1,
                                    IsRequired = false,
                                    Genre = 1,
                                    Type = 1
                                }
                            }
                        },
                    },
                    Questions = new List<Question>()
                        {
                            new Question
                            {
                                Header="Question One in Form",
                                Description="Question One in Form Description",
                                Position= 1,
                                IsRequired = false,
                                Genre = 1,
                                Type = 1
                            }
                        }
                }
            };
        }
    }
}
