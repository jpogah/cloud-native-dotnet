﻿using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Services
{
    public class FortuneServiceCommand : HystrixCommand<Fortune>
    {
        IFortuneService _fortuneService;
        ILogger<FortuneServiceCommand> _logger;

        public FortuneServiceCommand(IHystrixCommandOptions options,
            IFortuneService fortuneService,
            ILogger<FortuneServiceCommand> logger) : base(options)
        {
            _fortuneService = fortuneService;
            _logger = logger;
            IsFallbackUserDefined = true;
        }

        public async Task<Fortune> RandomFortuneAsync()
        {
            return await ExecuteAsync();
        }

        protected override async Task<Fortune> RunAsync()
        {
            var result = await _fortuneService.RandomFortuneAsync();
            _logger.LogInformation("Run: {0}", result);
            return result;
        }

        protected override async Task<Fortune> RunFallbackAsync()
        {
            if (CommandResult.ExecutionOccurred)
            {
                _logger.LogError(CommandResult.ExecutionException, "Running fallback...");
            }

            _logger.LogInformation("RunFallback");
            return await Task.FromResult(new Fortune() { Id = 9999, Text = "You will have a happy day!" });
        }
    }
}
