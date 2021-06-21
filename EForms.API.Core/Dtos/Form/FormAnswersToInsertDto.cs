using EForms.API.Core.Dtos.Answer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Form
{
    public class FormAnswersToInsertDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<AnswerToInsertDto> Answers { get; set; }
    }
}
