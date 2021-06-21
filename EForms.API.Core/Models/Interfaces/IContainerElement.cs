using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EForms.API.Core.Models.Interfaces
{
    public interface IContainerElement : IElement
    {
        public int ColumnRepresentation { get; set; }
        public List<Question> Questions { get; set; }
    }
}
