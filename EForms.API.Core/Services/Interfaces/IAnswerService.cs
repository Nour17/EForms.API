using EForms.API.Core.Models;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IAnswerService
    {
        public FormAnswersCore ValidateFormAnswers(FormCore form, FormAnswersCore formAnswers);
    }
}
