using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models
{
    public class ErrorMessage
    {
        public string QuestionId { get; set; }
        public string Message { get; set; }
    }
}
