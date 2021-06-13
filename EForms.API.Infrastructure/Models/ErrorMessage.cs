using EForms.API.Infrastructure.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models
{
    public class ErrorMessage
    {
        public string QuestionId { get; set; }
        public string Content { get; set; }
    }
}
