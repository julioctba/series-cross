using System;
using System.Collections.Generic;
using Okra.Models;

namespace Okra.Repositories
{
    public interface ILocalDataBaseRepository
    {
        void Add(Serie serie);
        void Edit(Serie serie);
        void Delete(Serie serie);

        List<Serie> GetAll();

        Serie GetById(Serie serie);
    }
}
