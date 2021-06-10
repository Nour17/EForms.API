using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Infrastructure.Models
{
    public class Answer : IAnsweredFormResponse
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
        public string Content { get; set; }
    }
}
