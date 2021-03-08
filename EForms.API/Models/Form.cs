﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models
{
    public class Form
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string InternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; } = 1;
        public List<Section> Sections { get; set; }
        public List<Question> Questions { get; set; }
        // UserId: Array of Answers
        public List<Dictionary<String, List<Answer>>> FormAnswers { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
