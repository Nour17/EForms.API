using EForms.API.Core.Dtos.Answer;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Form
{
    public class FormAnswersToReturnDto
    {
        public string UserId { get; set; }
        public bool IsValid { get; set; } = true;
        public List<AnswerToReturnDto> Answers { get; set; } = new List<AnswerToReturnDto>();
    }
}
