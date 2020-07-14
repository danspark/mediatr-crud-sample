using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Infrastructure
{
    public class DomainEventPublisher
    {
        private readonly ILogger<DomainEventPublisher> _logger;

        public DomainEventPublisher(ILogger<DomainEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(object eventData, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain event published: {eventData}", eventData);

            return Task.CompletedTask;
        }
    }
}
