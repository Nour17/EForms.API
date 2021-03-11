using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models.Interfaces
{
    interface IContainerElement
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
