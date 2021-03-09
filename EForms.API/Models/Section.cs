using EForms.API.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models
{
    public class Section : IContainerElement
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; } = 1;
        public List<Question> Questions { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
