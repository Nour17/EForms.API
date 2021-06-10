using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models.Interfaces
{
    public interface IAnsweredFormResponse
    {
        public string QuestionId { get; set; }
        public string Content { get; set; }
    }
}
