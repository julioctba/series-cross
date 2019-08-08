using Okra.Infra;
using Okra.Infra.Api;
using Okra.Models;
using Okra.Repositories;
using Okra.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okra.Services
{
    public class SerieServiceDB : ISerieServiceDB
    {

        private readonly ILocalDataBaseRepository localDataBaseRepository;

        public SerieServiceDB(ILocalDataBaseRepository localDataBaseRepository)
        {
            this.localDataBaseRepository = localDataBaseRepository;
        }

        public Task Add(Serie serie)
            => Task.Run(() => localDataBaseRepository.Add(serie));

        public Task<List<Serie>> All()
            => Task.Run(() => localDataBaseRepository.GetAll());
    }
}
