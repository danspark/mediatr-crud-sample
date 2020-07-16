using MediatR.Pipeline;
using MediatR.Sample.Core.Commands;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Exceptions;
using MediatR.Sample.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class ProductCreationValidator : IRequestPreProcessor<CreateEntityCommand<Product>>
    {
        private readonly DbContext _context;

        public ProductCreationValidator(SamplesContext context)
        {
            _context = context;
        }

        public async Task Process(CreateEntityCommand<Product> request, CancellationToken cancellationToken)
        {
            if (request.Entity.CategoryId is null) return;

            var category = await _context.Set<Category>()
                .SingleOrDefaultAsync(c => c.Id == request.Entity.CategoryId, cancellationToken);

            if (category is null) throw new EntityNotFoundException();
        }
    }
}
