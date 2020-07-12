using MediatR.Sample.Core.Commands.Abstractions;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Commands
{
    public class CreateEntityCommand<T> : IRequest<T>, ITransactionalCommand where T : Entity
    {
        public CreateEntityCommand(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }

        public class Handler : IRequestHandler<CreateEntityCommand<T>, T>
        {
            private readonly SamplesContext _context;

            public Handler(SamplesContext context)
            {
                _context = context;
            }

            public async Task<T> Handle(CreateEntityCommand<T> request, CancellationToken cancellationToken)
            {
                _context.Add(request.Entity);

                await _context.SaveChangesAsync(cancellationToken);

                return request.Entity;
            }
        }
    }
}
