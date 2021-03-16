using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Infrastructure.Models
{
    public class Answer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
        public string Header { get; set; }
        public string UserAnswer { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
