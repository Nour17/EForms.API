using EForms.API.Core.Models.Interfaces;

namespace EForms.API.Core.Models
{
    public class AnswerCore : IAnswerCore
    {
        public string QuestionId { get; set; }
        public string UserAnswer { get; set; }
        public string Message { get; set; }
    }
}
