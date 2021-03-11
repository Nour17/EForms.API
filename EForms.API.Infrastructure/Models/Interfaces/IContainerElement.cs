using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models.Interfaces
{
    public interface IContainerElement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<Question> Questions { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; }
    }
}
