using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EForms.API.Dtos.Answer;

namespace EForms.API.Dtos.Form
{
    public class FormAnswersToInsertDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<AnswerToInsertDto> Answers { get; set; }
    }
}
