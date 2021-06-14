using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Core.Models
{
    public class AnsweredForm
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string UserId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
