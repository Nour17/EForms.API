using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Answer
{
    public class AnswerToInsertDto
    {
        [Required]
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public List<string> Answers { get; set; }
    }
}
