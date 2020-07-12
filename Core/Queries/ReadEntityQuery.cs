using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Sample.Core.Queries
{
    public class ReadEntityQuery<T> : IRequest<T> where T : Entity
    {
        public Guid EntityId { get; }

        public ReadEntityQuery(Guid entityId)
        {
            EntityId = entityId;
        }

        public class Handler : IRequestHandler<ReadEntityQuery<T>, T>
        {
            private readonly SamplesContext _context;

            public Handler(SamplesContext context)
            {
                _context = context;
            }

            public async Task<T> Handle(ReadEntityQuery<T> request, CancellationToken cancellationToken)
            {
                return await _context.Set<T>()
                    .SingleOrDefaultAsync(x => x.Id == request.EntityId, cancellationToken);
            }
        }
    }
}
