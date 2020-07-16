using MediatR.Sample.Core.Commands;
using MediatR.Sample.Core.Entities;
using MediatR.Sample.Core.Exceptions;
using MediatR.Sample.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CrudController<T> : ControllerBase where T : Entity
    {
        private readonly IMediator _mediator;

        protected CrudController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Create([FromBody] T entity, CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new CreateEntityCommand<T>(entity), cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> Read(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new ReadEntityQuery<T>(id), cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<T>> Update(Guid id, [FromBody] T entity, CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new UpdateEntityCommand<T>(id, entity), cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new DeleteEntityCommand<T>(id), cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public virtual async Task<ActionResult<IReadOnlyList<T>>> List(int skip, int take, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListEntitiesQuery<T>
            {
                Skip = skip,
                Take = take
            }, cancellationToken);

            return Ok(result);
        }
    }
}
