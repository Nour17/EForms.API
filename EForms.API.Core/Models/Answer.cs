using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EForms.API.Core.Models
{
    public class Answer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
        public string Content { get; set; }
    }
}
