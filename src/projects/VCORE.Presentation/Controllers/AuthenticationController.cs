using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VCORE.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        
    }
}
