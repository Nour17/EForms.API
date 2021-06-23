using EForms.API.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IFormService
    {
        Task<FormCore> AddForm(FormCore formToAdd);
        Task<FormCore> GetForm(string id);
        Task<bool> UpdateForm(string id, FormCore updatedForm);
        Task<List<FormCore>> GetForms();
        bool IsValid(FormCore formToInsert);
    }
}
