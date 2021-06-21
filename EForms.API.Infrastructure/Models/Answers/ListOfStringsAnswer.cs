using EForms.API.Infrastructure.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models.Answers
{
    public class ListOfStringsAnswer : Answer
    {
        public List<string> Answers { get; set; }
    }
}
