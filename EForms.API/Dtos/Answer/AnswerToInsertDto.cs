using System.ComponentModel.DataAnnotations;

namespace EForms.API.Dtos.Answer
{
    public class AnswerToInsertDto
    {
        [Required]
        public string QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
