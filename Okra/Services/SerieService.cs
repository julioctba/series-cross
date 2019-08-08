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
    public class SerieService : ISerieService
    {
        readonly ITmdbApi _api;

        public SerieService(ITmdbApi api)
        {
            _api = api;
        }


        public async Task<SerieResponse> GetSeriesAsync()
        {
            return await _api.GetSerieResponseAsync(AppSetting.ApiKey);
        }

    }
}
