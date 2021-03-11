using EForms.API.Helpers;
using EForms.API.Models;
using EForms.API.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMongoCollection<Question> _question;

        public QuestionRepository(IOptions<DbSettings> settings)
        {
            _context = new DataContext(settings);
            _question = _context.database.GetCollection<Question>("Questions");
        }

        public async Task<List<Question>> GetQuestions<T>()
        {
            return await _question.Find(_ => true).ToListAsync();
        }

        public async Task<Question> GetQuestion<T>(string id)
        {
            return await _question.Find(x => x.InternalId == id).FirstOrDefaultAsync();
        }

        public async Task<Question> AddQuestion<T>(Question question)
        {
            await _question.InsertOneAsync(question);
            return question;
        }

        public async Task<bool> UpdateQuestion<T>(string id, Question questionIn)
        {
            ReplaceOneResult actionResult = await _question.ReplaceOneAsync(x => x.InternalId == id, questionIn);

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveQuestion<T>(string id)
        {
            DeleteResult actionResult = await _question.DeleteOneAsync(x => x.InternalId == id);

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }
    }
}
