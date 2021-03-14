﻿using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models
{
    public class Form : IContainerElement
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
        public List<FormAnswer> FormAnswers { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}