using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Models.Interfaces
{
    public interface IElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; }
    }
}
