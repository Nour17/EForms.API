using EForms.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EForms.API.Data
{
    public class DataContext : DbContext
    {
        public readonly IMongoDatabase database = null;
        public DataContext(IOptions<DbSettings> settings) 
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if(client != null)
            {
                database = client.GetDatabase(settings.Value.Database);
            } 
        }
    }
}
