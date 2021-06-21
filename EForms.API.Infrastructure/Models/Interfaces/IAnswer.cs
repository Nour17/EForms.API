using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models.Interfaces
{
    public interface IAnswer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
    }
}
