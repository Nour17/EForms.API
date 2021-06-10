using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Answer
{
    public class FormAnswerDto
    {
        [Required]
        public string QuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
