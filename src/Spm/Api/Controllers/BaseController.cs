using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger _logger;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected ILogger Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger>());
        // Same with dependency injection
    }
}
