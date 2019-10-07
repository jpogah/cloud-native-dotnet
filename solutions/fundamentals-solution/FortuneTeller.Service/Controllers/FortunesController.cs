using FortuneTeller.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FortuneTeller.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FortunesController : ControllerBase
    {
        private readonly ILogger<FortunesController> _logger;
        private IFortuneRepository _fortunes;

        public FortunesController(ILogger<FortunesController> logger, IFortuneRepository fortunes)
        {
            _logger = logger;
            _fortunes = fortunes;
        }

        // GET: api/fortunes/all
        [HttpGet("all")]
        public async Task<List<Fortune>> AllFortunesAsync()
        {
            _logger?.LogTrace("AllFortunesAsync");
            var entities = await _fortunes.GetAllAsync();
            return entities
                    .Select(fortune => new Fortune { Id = fortune.Id, Text = fortune.Text })
                    .ToList();
        }

        // GET api/fortunes/random
        [HttpGet("random")]
        public async Task<Fortune> RandomFortuneAsync()
        {
            _logger?.LogTrace("RandomFortuneAsync");
            var fortuneEntity = await _fortunes.RandomFortuneAsync();
            return new Fortune { Id = fortuneEntity.Id, Text = fortuneEntity.Text };
        }
    }
}
