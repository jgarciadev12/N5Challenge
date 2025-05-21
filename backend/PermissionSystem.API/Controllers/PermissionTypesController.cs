using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.Permissions.Dtos;
using PermissionSystem.Application.Permissions.Queries;

namespace PermissionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PermissionTypeDto>>> Get()
        {
            var result = await _mediator.Send(new GetPermissionTypesQuery());
            return Ok(result);
        }
    }
}
