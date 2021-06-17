using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EForms.API.Infrastructure.Models.Answers
{
    public class StringAnswer : Answer
    {
        public string Answer { get; set; }
    }
}
