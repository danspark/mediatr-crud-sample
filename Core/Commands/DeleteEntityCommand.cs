using MediatR.Sample.Core.Commands.Abstractions;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Commands
{
    public class DeleteEntityCommand<T> : IRequest<T>, ITransactionalCommand where T : Entity
    {
        public DeleteEntityCommand(Guid entityId)
        {
            EntityId = entityId;
        }

        public Guid EntityId { get; }

        public class Handler : IRequestHandler<DeleteEntityCommand<T>, T>
        {
            private readonly SamplesContext _context;

            public Handler(SamplesContext context)
            {
                _context = context;
            }

            public async Task<T> Handle(DeleteEntityCommand<T> request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == request.EntityId, cancellationToken);
                if (entity is null) return null;

                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Deleted;

                await _context.SaveChangesAsync(cancellationToken);

                return entity;

            }
        }
    }
}
