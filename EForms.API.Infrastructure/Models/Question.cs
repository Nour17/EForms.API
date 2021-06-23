﻿using EForms.API.Infrastructure.Enums;
using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EForms.API.Infrastructure.Models
{
    public class Question : IElement, IContained
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public bool IsRequired { get; set; } = false;
        public QuestionGenre Genre { get; set; }
        public QuestionType Type { get; set; }
        [BsonIgnoreIfNull]
        public Options Options { get; set; }
        [BsonIgnoreIfNull]
        public Range Range { get; set; }
        [BsonIgnoreIfNull]
        public Restriction Restriction { get; set; }
        public bool IsOptionBased()
        {
            if (Type == QuestionType.Dropdown
             || Type == QuestionType.RadioButton
             || Type == QuestionType.CheckBox)
                return true;
            return false;
        }
        public bool IsRangeBased()
        {
            if (Type == QuestionType.RangeInput)
                return true;
            return false;
        }
    }
}
