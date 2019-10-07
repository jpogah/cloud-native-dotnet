using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Services
{
    public class FortuneServiceClient : IFortuneService
    {
        private readonly ILogger<FortuneServiceClient> _logger;
        private IOptions<FortuneServiceOptions> _config;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
            ILogger<FortuneServiceClient> logger,
            IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
            _httpContextAccessor = contextAccessor;

        }

        public async Task<List<Fortune>> AllFortunesAsync()
        {
            var authenticatedClient = await AttachUserTokenAsync(_httpClient, _httpContextAccessor.HttpContext);
            var response = await authenticatedClient.GetAsync(Config.AllFortunesURL);
            return await response.Content.ReadAsAsync<List<Fortune>>();
            
        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            var authenticatedClient = await AttachUserTokenAsync(_httpClient, _httpContextAccessor.HttpContext);
            var response = await authenticatedClient.GetAsync(Config.RandomFortuneURL);
            return await response.Content.ReadAsAsync<Fortune>();
        }

        private async Task<HttpClient> AttachUserTokenAsync(HttpClient httpClient, HttpContext httpContext)
        {
            var token = await httpContext.GetTokenAsync("access_token");
            _logger?.LogDebug("AttachUserTokenAsync found access token: {token}", token);

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }
    }
}