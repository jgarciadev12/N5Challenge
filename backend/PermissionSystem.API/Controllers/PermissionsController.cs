using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.Permissions.Commands;
using PermissionSystem.Application.Permissions.Dtos;
using PermissionSystem.Application.Permissions.Queries;

namespace PermissionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Request a new permission
        /// </summary>
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> RequestPermission([FromBody] CreatePermissionDto dto)
        {
            var command = new CreatePermissionCommand
            {
                EmployeeName = dto.EmployeeName,
                EmployeeLastName = dto.EmployeeLastName,
                PermissionDate = dto.PermissionDate,
                PermissionTypeId = dto.PermissionTypeId
            };

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPermissionById), new { id }, id);
        }

        /// <summary>
        /// Modify an existing permission
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPermission(int id, [FromBody] UpdatePermissionDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var command = new ModifyPermissionCommand
            {
                Id = dto.Id,
                EmployeeName = dto.EmployeeName,
                EmployeeLastName = dto.EmployeeLastName,
                PermissionDate = dto.PermissionDate,
                PermissionTypeId = dto.PermissionTypeId
            };

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var result = await _mediator.Send(new GetPermissionsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get permission by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _mediator.Send(new GetPermissionByIdQuery { Id = id });
            if (permission == null)
                return NotFound();

            return Ok(permission);
        }
    }
}
