using MediatR;
using Microsoft.AspNetCore.Mvc;
using VCORE.Application.Features.Teachers.Commands.Create;
using VCORE.Application.Features.Teachers.Commands.Delete;
using VCORE.Application.Features.Teachers.Commands.Update;
using VCORE.Application.Features.Teachers.Queries.GetAll;
using VCORE.Application.Features.Teachers.Queries.GetById;

namespace VCORE.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(TeacherAddCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(TeacherDeleteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(TeacherUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new TeacherGetAllQuery()));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await mediator.Send(new TeacherGetByIdQuery() { Id = id }));
        }
    }
}