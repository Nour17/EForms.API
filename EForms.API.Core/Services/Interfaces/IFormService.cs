using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Question;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IFormService
    {
        Task<Form> GetForm(string id);
        Task<List<Form>> GetForms();
        List<ErrorMessage> ValidateFormAnswers(ref Form form, FormAnswersDto formAnswers);
        void IsReceivedFormValid(FormToInsertDto formToInsert);
    }
}
