using EForms.API.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Repository.Data.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task<List<Question>> GetQuestions<T>();
        Task<Question> GetQuestion<T>(string id);
        Task<Question> AddQuestion<T>(Question question);
        Task<bool> UpdateQuestion<T>(string id, Question questionIn);
        Task<bool> RemoveQuestion<T>(string id);
    }
}
