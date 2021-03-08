﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models
{
    public enum QuestionGenre
    {
        TextBased = 1,
        OptionBased = 2,
        FileBased = 3
    }

    public enum QuestionType
    {
        NormalText = 1,
        Email = 2,
        URL = 3,
        Number = 4,
        Date = 5,
        Time = 6,
        Dropdown = 7,
        RadioButton = 8,
        CheckBox = 9,
        RangeInput = 10,
        File = 11
    }

    public class Question
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Header { get; set; } = "Untitled";
        public string Description { get; set; }
        public bool IsRequired { get; set; } = false;
        public QuestionGenre Genre { get; set; }
        public QuestionType Type { get; set; }
        public Restriction Restriction { get; set; }
        // UserId: One Answer
        public List<Dictionary<String, Answer>> QuestionAnswers { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
