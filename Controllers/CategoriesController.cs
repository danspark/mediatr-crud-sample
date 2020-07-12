using MediatR.Sample.Core.Entities;

namespace MediatR.Sample.Controllers
{
    public class CategoriesController : CrudController<Category>
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }
    }
}
