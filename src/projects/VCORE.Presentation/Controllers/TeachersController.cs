using MediatR;
using Microsoft.AspNetCore.Mvc;
using VCORE.Application.Features.Teachers.Commands.Create;
using VCORE.Application.Features.Teachers.Commands.CreateRange;
using VCORE.Application.Features.Teachers.Commands.Delete;
using VCORE.Application.Features.Teachers.Commands.DeleteRange;
using VCORE.Application.Features.Teachers.Commands.HardDelete;
using VCORE.Application.Features.Teachers.Commands.HardDeleteRange;
using VCORE.Application.Features.Teachers.Commands.Update;
using VCORE.Application.Features.Teachers.Commands.UpdateRange;
using VCORE.Application.Features.Teachers.Queries.GetAll;
using VCORE.Application.Features.Teachers.Queries.GetAllDetail;
using VCORE.Application.Features.Teachers.Queries.GetById;
using VCORE.Application.Features.Teachers.Queries.GetByName;
using VCORE.Application.Features.Teachers.Queries.GetCount;
using VCORE.Application.Features.Teachers.Queries.GetDetailById;

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

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(TeacherAddRangeCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(TeacherDeleteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("DeleteRange")]
        public async Task<IActionResult> DeleteRange(TeacherDeleteRangeCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("HardDelete")]
        public async Task<IActionResult> HardDelete(TeacherHardDeleteCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("HardDeleteRange")]
        public async Task<IActionResult> HardDeleteRange(TeacherHardDeleteRangeCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(TeacherUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPut("UpdateRange")]
        public async Task<IActionResult> UpdateRange(TeacherUpdateRangeCommand command)
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

        [HttpGet("GetAllDetail")]
        public async Task<IActionResult> GetAllDetail()
        {
            return Ok(await mediator.Send(new TeacherGetAllDetailQuery()));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string fullName)
        {
            return Ok(await mediator.Send(new TeacherGetByNameQuery() { FullName = fullName }));
        }

        [HttpGet("GetCount")]
        public async Task<IActionResult> GetByName()
        {
            return Ok(await mediator.Send(new TeacherGetCountQuery()));
        }

        [HttpGet("GetDetailById")]
        public async Task<IActionResult> GetDetailById(Guid id)
        {
            return Ok(await mediator.Send(new TeacherGetDetailByIdQuery() { Id = id }));
        }
    }
}