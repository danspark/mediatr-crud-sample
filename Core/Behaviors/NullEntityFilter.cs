using MediatR.Pipeline;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class NullEntityFilter<TReq,TRes> : IRequestPostProcessor<TReq, TRes>
        where TRes : Entity
    {
        public Task Process(TReq request, TRes response, CancellationToken cancellationToken)
        {
            if (response is null) throw new EntityNotFoundException();

            return Task.CompletedTask;
        }
    }
}
