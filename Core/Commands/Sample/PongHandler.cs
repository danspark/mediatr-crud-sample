using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Commands.Sample
{
    public class PongHandler : IRequestHandler<PingRequest, string>
    {
        public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
        {
            request.Stopwatch.Stop();

            return Task.FromResult($"Pong! Elapsed time: {request.Stopwatch.Elapsed}");
        }
    }
}
