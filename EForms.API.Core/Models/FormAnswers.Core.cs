using System;
using System.Collections.Generic;

namespace EForms.API.Core.Models
{
    public class FormAnswersCore
    {
        public string InternalId { get; set; }
        public bool IsValid { get; set; } = true;
        public string UserId { get; set; }
        public List<AnswerCore> Answers { get; set; } = new List<AnswerCore>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
