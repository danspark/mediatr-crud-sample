using MediatR.Sample.Core.Commands.Abstractions;
using MediatR.Sample.Core.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Core.Behaviors
{
    public class TransactionalBehavior<TReq, TRes> : IPipelineBehavior<TReq, TRes> where TReq : ITransactionalCommand
    {
        private readonly SamplesContext _context;

        public TransactionalBehavior(SamplesContext context)
        {
            _context = context;
        }

        public async Task<TRes> Handle(TReq request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TRes> next)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await next();

                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}