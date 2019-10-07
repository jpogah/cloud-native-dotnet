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
        private readonly IFortuneRepository _fortuneRepository;

        public FortunesController(ILogger<FortunesController> logger, IFortuneRepository fortuneRepository)
        {
            _logger = logger;
            _fortuneRepository = fortuneRepository;
        }

        // GET: api/fortunes/all
        [HttpGet("all")]
        public async Task<List<Fortune>> AllFortunesAsync()
        {
            _logger?.LogTrace("AllFortunesAsync");
            var entities = await  _fortuneRepository.GetAllAsync();
            return entities.Select(fortune => new Fortune { Id = fortune.Id, Text = fortune.Text }).ToList();


            //return await Task.FromResult(new List<Fortune>() { new Fortune() { Id = 1, Text = "Hello from FortuneController Web API!" } });
        }

        // GET api/fortunes/random
        [HttpGet("random")]
        public async Task<Fortune> RandomFortuneAsync()
        {
            _logger?.LogTrace("RandomFortuneAsync");
            var entity = await _fortuneRepository.RandomFortuneAsync();
            return new Fortune()
            {
                Id = entity.Id,
                Text = entity.Text
            };
           
        }
    }
}
