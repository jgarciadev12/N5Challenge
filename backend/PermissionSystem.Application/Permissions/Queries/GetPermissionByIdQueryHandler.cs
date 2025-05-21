using MediatR;
using PermissionSystem.Application.Permissions.Dtos;
using PermissionSystem.Domain.Repositories;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto?>
    {
        private readonly IPermissionRepository _repository;

        public GetPermissionByIdQueryHandler(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PermissionDto?> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var permission = await _repository.GetByIdAsync(request.Id);
            if (permission == null)
                return null;

            var dto = new PermissionDto
            {
                Id = permission.Id,
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionDate = permission.PermissionDate,
                PermissionTypeDescription = permission.PermissionType?.Description ?? "N/A"
            };

            return dto;
        }
    }
}
