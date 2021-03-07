using EForms.API.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Data.Repositories.Interfaces
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
