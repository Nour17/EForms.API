using EForms.API.Core.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Core.Models
{
     public class Question : IElement
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public bool IsRequired { get; set; } = false;
        public int Genre { get; set; }
        public int Type { get; set; }
        public Options Options { get; set; }
        public Range Range { get; set; }
        public Restriction Restriction { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
