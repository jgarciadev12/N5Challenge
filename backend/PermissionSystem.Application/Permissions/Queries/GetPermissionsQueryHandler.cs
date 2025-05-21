using MediatR;
using PermissionSystem.Application.Permissions.Dtos;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
    {
        private readonly IPermissionRepository _repository;
        private readonly IMessagingService _messaging;
        private readonly IElasticsearchService _elasticsearch;

        public GetPermissionsQueryHandler(
            IPermissionRepository repository,
            IMessagingService messaging,
            IElasticsearchService elasticsearch)
        {
            _repository = repository;
            _messaging = messaging;
            _elasticsearch = elasticsearch;
        }

        public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _repository.GetAllAsync();

            var dtoList = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                EmployeeName = p.EmployeeName,
                EmployeeLastName = p.EmployeeLastName,
                PermissionDate = p.PermissionDate,
                PermissionTypeDescription = p.PermissionType?.Description ?? "N/A"
            }).ToList();

            var eventDto = new PermissionEventDto { NameOperation = "get" };

            await _messaging.SendEventAsync(eventDto);
            await _elasticsearch.IndexPermissionEventAsync(eventDto);
            return dtoList;
        }
    }
}
