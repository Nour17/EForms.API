using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models
{
    public class FormAnswer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string UserId { get; set; }
        public List<Answer> Answers { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
