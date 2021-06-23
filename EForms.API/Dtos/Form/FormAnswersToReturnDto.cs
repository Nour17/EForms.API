using EForms.API.Dtos.Answer;
using System.Collections.Generic;

namespace EForms.API.Dtos.Form
{
    public class FormAnswersToReturnDto
    {
        public string UserId { get; set; }
        public bool IsValid { get; set; } = true;
        public List<AnswerToReturnDto> Answers { get; set; } = new List<AnswerToReturnDto>();
    }
}
