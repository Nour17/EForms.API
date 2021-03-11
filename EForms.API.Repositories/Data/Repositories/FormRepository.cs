using EForms.API.Helpers;
using EForms.API.Models;
using EForms.API.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly DataContext _context;
        private readonly IMongoCollection<Form> _form;

        public FormRepository(IOptions<DbSettings> settings)
        {
            _context = new DataContext(settings);
            _form = _context.database.GetCollection<Form>("Forms");
        }

        public async Task<Form> AddForm<T>(Form form)
        {
            await _form.InsertOneAsync(form);
            return form;
        }

        public async Task<Form> GetForm<T>(string id)
        {
            return await _form.Find(x => x.InternalId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Form>> GetForms<T>()
        {
            return await _form.Find(_ => true).ToListAsync();
        }

        public async Task<bool> RemoveForm<T>(string id)
        {
            DeleteResult actionResult = await _form.DeleteOneAsync(x => x.InternalId == id);

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateForm<T>(string id, Form form)
        {
            ReplaceOneResult actionResult = await _form.ReplaceOneAsync(x => x.InternalId == id, form, new ReplaceOptions() { IsUpsert = true });

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }
    }
}
