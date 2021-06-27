using System.ComponentModel.DataAnnotations;

namespace EForms.API.Dtos.Answer
{
    public class AnswerToInsertDto
    {
        [Required]
        public string QuestionId { get; set; }
        public string UserAnswer { get; set; }
    }
}
