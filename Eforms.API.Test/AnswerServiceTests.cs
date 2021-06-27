using EForms.API.Core.Models;
using EForms.API.Core.Services;
using Xunit;

namespace EForms.API.Test
{
    public class AnswerServiceTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCorrectFormFromDataGeneraor), MemberType = typeof(TestDataGenerator))]
        public void CreateContainer_ValidFormShouldWork(FormCore form, FormAnswersCore formAnswersCore)
        {
            // Arange
            AnswerService answerService = new AnswerService();
            var validatedForm = answerService.ValidateFormAnswers(form, formAnswersCore);
            bool isValid = validatedForm.IsValid;
            // Actual
            // Assert
            Assert.True(isValid);
        }
    }
}
