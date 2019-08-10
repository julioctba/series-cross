using Okra.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okra.Services
{
    public interface ISerieServiceDB
    {

        Task Add(Serie serie);

        Task Delete(Serie serie);

        Task<Serie> GetById(Serie serie);

        Task<List<Serie>> All();
    }
}
