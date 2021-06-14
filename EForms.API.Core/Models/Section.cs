
using EForms.API.Core.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace EForms.API.Core.Models
{
    public class Section : IContainerElement
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
