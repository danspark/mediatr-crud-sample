using MediatR.Sample.Core.Commands.Abstractions;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Commands
{
    public class UpdateEntityCommand<T> : IRequest<T>, ITransactionalCommand where T : Entity
    {
        public UpdateEntityCommand(Guid entityId, T entity)
        {
            EntityId = entityId;
            Entity = entity;
        }

        public Guid EntityId { get;}

        public T Entity { get; }

        public class Handler : IRequestHandler<UpdateEntityCommand<T>, T>
        {
            private readonly SamplesContext _context;

            public Handler(SamplesContext context)
            {
                _context = context;
            }

            public async Task<T> Handle(UpdateEntityCommand<T> request, CancellationToken cancellationToken)
            {
                var exists = await _context.Set<T>().AnyAsync(x => x.Id == request.EntityId, cancellationToken);
                if (!exists) return null;

                var entity = request.Entity;
                entity.Id = request.EntityId;

                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
