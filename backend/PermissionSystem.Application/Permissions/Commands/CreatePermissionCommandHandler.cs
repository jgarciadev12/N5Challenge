using MediatR;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Application.Permissions.Commands
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    {
        private readonly IPermissionRepository _repository;
        private readonly IElasticsearchService _elasticsearch;
        private readonly IMessagingService _messaging;

        public CreatePermissionCommandHandler(IPermissionRepository repository,
            IElasticsearchService elasticsearch, IMessagingService messaging)
        {
            _repository = repository;
            _elasticsearch = elasticsearch;
            _messaging = messaging;
        }

        public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                EmployeeName = request.EmployeeName,
                EmployeeLastName = request.EmployeeLastName,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = request.PermissionDate
            };

            await _repository.AddAsync(permission);
            await _repository.SaveChangesAsync();

            await _elasticsearch.IndexPermissionAsync(permission);

            await _messaging.SendEventAsync(new PermissionEventDto { NameOperation = "request" });

            return permission.Id;
        }
    }
}
