using MediatR.Sample.Core.Entities;

namespace MediatR.Sample.Controllers
{
    public class ProductsController : CrudController<Product>
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }
    }
}