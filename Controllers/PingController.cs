using MediatR.Sample.Core.Commands.Sample;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediatR.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<string> Ping() => _mediator.Send(new PingRequest());
    }
}
