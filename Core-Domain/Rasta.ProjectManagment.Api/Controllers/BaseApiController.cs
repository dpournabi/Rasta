using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Rasta.ProjectManagment.Api.Controllers
{
    [ApiController]
    //[DisableCors]
    [EnableCors("RastaCorsPolicy")]//(Constants.CorsName)]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator == null ? HttpContext.RequestServices.GetRequiredService<ISender>() : _mediator;
                
    }
}
