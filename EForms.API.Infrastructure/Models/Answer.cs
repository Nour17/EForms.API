using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Infrastructure.Models
{
    public class Answer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Header { get; set; }
        public string UserAnswer { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
