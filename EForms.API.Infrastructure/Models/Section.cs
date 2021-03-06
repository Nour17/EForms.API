using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models
{
    public class Section : IContainerElement
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; } = 1;
        public List<Question> Questions { get; set; } = new List<Question>();
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
