using EForms.API.Core.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace EForms.API.Core.Models
{
    public class Form : IContainerElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; } = 0;
        public int ColumnRepresentation { get; set; } = 1;
        public List<Section> Sections { get; set; } = new List<Section>();
        public List<Question> Questions { get; set; } = new List<Question>();
        // UserId: Array of Answers
        public List<AnsweredForm> FormAnswers { get; set; } = new List<AnsweredForm>();
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
