using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Dtos.Question
{
    public class QuestionToAnswerDto
    {
        public string SectionId { get; set; }
        public string QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
