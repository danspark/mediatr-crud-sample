using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core.Enrichers;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class PreProcessorLoggingBehavior<TReq> : IRequestPreProcessor<TReq>
    {
        private readonly ILogger<TReq> _logger;

        public PreProcessorLoggingBehavior(ILogger<TReq> logger)
        {
            _logger = logger;
        }

        public Task Process(TReq request, CancellationToken cancellationToken)
        {
            using var context = LogContext.Push(new PropertyEnricher(nameof(request), request, true));
            
            _logger.LogInformation("Request starting: {request}", request);

            return Task.CompletedTask;
        }
    }
}
