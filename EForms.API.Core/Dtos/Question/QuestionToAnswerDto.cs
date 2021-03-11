namespace EForms.API.Core.Dtos.Question
{
    public class QuestionToAnswerDto
    {
        public string UserId { get; set; }
        public string SectionId { get; set; }
        public string QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
