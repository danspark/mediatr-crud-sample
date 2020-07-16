using System.Diagnostics;

namespace MediatR.Sample.Core.Commands.Sample
{
    public class PingRequest : IRequest<string>
    {
        public Stopwatch Stopwatch { get; } = Stopwatch.StartNew();
    }
}
