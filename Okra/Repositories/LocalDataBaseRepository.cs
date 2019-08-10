using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Okra.Models;
using Okra.Services;

namespace Okra.Repositories
{
    public sealed class LocalDataBaseRepository : ILocalDataBaseRepository
    {
        private const string COLLECTION_NAME = "series";

        private readonly LiteCollection<Serie> liteCollection;
        private readonly IDataBaseAccessService dataBaseAccessService;

        public LocalDataBaseRepository(IDataBaseAccessService dataBaseAccessService)
        {
            this.dataBaseAccessService = dataBaseAccessService;
            liteCollection = GetCollection();
        }

        public void Add(Serie serie)
            => liteCollection.Insert(serie);

        public void Delete(Serie serie)
            => liteCollection.Delete(serie.Id);

        public void Edit(Serie serie)
            => liteCollection.Update(serie);

        public List<Serie> GetAll()
            => liteCollection.FindAll().ToList();

        public Serie GetById(Serie serie)
            => liteCollection.FindById(serie.Id);
  

        private LiteCollection<Serie> GetCollection()
        {
            var db = GetOrCreateLiteDatabase();
            return db.GetCollection<Serie>(COLLECTION_NAME);
        }

        private LiteDatabase GetOrCreateLiteDatabase()
        {
            var dbPath = dataBaseAccessService.GetDataBasePath();
            return new LiteDatabase($@"{dbPath}");
        }
    }
}
