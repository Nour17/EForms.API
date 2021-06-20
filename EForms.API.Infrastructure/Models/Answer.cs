using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models
{
    public class Answer : IAnswer
    {
        public string QuestionId { get; set; }
        public string UserAnswer { get; set; }
    }
}
