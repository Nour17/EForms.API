using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Infrastructure.Models
{
    public class Answer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
        public string UserAnswer { get; set; }
    }
}
