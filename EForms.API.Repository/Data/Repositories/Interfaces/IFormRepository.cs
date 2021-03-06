using EForms.API.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Repository.Data.Repositories.Interfaces
{
    public interface IFormRepository
    {
        Task<List<Form>> GetForms<T>();
        Task<Form> GetForm<T>(string id);
        Task<Form> AddForm<T>(Form form);
        Task<bool> UpdateForm<T>(string id, Form form);
        Task<bool> RemoveForm<T>(string id);
    }
}
