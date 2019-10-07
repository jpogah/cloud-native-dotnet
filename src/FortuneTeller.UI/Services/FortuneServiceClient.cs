using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Services
{
    public class FortuneServiceClient : IFortuneService
    {
        private readonly ILogger<FortuneServiceClient> _logger;
        private IOptions<FortuneServiceOptions> _config;
        private readonly HttpClient _httpClient;

        private FortuneServiceOptions Config
        {
            get
            {
                return _config.Value;
            }
        }

        public FortuneServiceClient(
            HttpClient httpClient,
            IOptions<FortuneServiceOptions> config, 
            ILogger<FortuneServiceClient> logger)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<List<Fortune>> AllFortunesAsync()
        {
            var response = await _httpClient.GetAsync(Config.AllFortunesURL);
            return await response.Content.ReadAsAsync<List<Fortune>>();
            
        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            var response = await _httpClient.GetAsync(Config.RandomFortuneURL);
            return await response.Content.ReadAsAsync<Fortune>();
        }
    }
}