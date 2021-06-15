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
        Task<Form> AddForm(FormToInsertDto formToAdd);
        Task<Form> GetForm(string id);
        Task<bool> UpdateForm(string id, Form updatedForm);
        Task<List<Form>> GetForms();
        Form ValidateFormAnswers(Form form, FormAnswersDto formAnswers);
        bool IsReceivedFormValid(FormToInsertDto formToInsert);
    }
}
