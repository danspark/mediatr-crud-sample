using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core.Enrichers;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class PostProcessorLoggingBehavior<TReq, TRes> : IRequestPostProcessor<TReq, TRes>
    {
        private readonly ILogger<TReq> _logger;

        public PostProcessorLoggingBehavior(ILogger<TReq> logger)
        {
            _logger = logger;
        }

        public Task Process(TReq request, TRes response, CancellationToken cancellationToken)
        {
            using var context = LogContext.Push(new PropertyEnricher(nameof(request), request, true),
                new PropertyEnricher(nameof(response), response, true));

            _logger.LogInformation("Request finished: {request}. Response: {response}", request, response);

            return Task.CompletedTask;
        }
    }
}