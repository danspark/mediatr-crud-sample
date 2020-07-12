using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Queries
{
    public class ListEntitiesQuery<T> : IRequest<IReadOnlyList<T>> where T : Entity
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public class Handler : IRequestHandler<ListEntitiesQuery<T>, IReadOnlyList<T>>
        {
            private readonly SamplesContext _context;

            public Handler(SamplesContext context)
            {
                _context = context;
            }

            public async Task<IReadOnlyList<T>> Handle(ListEntitiesQuery<T> request, CancellationToken cancellationToken)
            {
                return await _context.Set<T>()
                    .OrderBy(x => x.CreationDate)
                    .Skip(Math.Max(request.Skip, 0))
                    .Take(Math.Max(Math.Min(request.Take, 100), 1))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
