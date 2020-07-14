using MediatR.Pipeline;
using MediatR.Sample.Core.Commands;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class PublishCreationEventBehavior<TReq, TResp> : IRequestPostProcessor<TReq, TResp> 
        where TReq : CreateEntityCommand<TResp> where TResp : Entity
    {
        private readonly IMediator _mediator;

        public PublishCreationEventBehavior(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Process(TReq request, TResp response, CancellationToken cancellationToken)
        {
            return _mediator.Publish(new EntityCreated<TResp>(response), cancellationToken);
        }
    }
}
