using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models.Interfaces
{
    public interface ITracker
    {
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }
        [BsonDateTimeOptions]
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
