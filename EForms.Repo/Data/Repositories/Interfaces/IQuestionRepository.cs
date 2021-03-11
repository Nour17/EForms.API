using EForms.API.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Data.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestions<T>();
        Task<Question> GetQuestion<T>(string id);
        Task<Question> AddQuestion<T>(Question question);
        Task<bool> UpdateQuestion<T>(string id, Question questionIn);
        Task<bool> RemoveQuestion<T>(string id);
    }
}
