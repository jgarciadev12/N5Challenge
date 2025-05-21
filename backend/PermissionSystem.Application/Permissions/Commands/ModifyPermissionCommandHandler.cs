using MediatR;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Application.Permissions.Commands
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, Unit>
    {
        private readonly IPermissionRepository _repository;
        private readonly IElasticsearchService _elasticsearch;
        private readonly IMessagingService _messaging;

        public ModifyPermissionCommandHandler(
            IPermissionRepository repository,
            IElasticsearchService elasticsearch,
            IMessagingService messaging)
        {
            _repository = repository;
            _elasticsearch = elasticsearch;
            _messaging = messaging;
        }

        public async Task<Unit> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _repository.GetByIdAsync(request.Id);
            if (permission == null)
            {
                throw new Exception($"Permission with id {request.Id} not found");
            }

            permission.EmployeeName = request.EmployeeName;
            permission.EmployeeLastName = request.EmployeeLastName;
            permission.PermissionTypeId = request.PermissionTypeId;
            permission.PermissionDate = request.PermissionDate;

            await _repository.UpdateAsync(permission);
            await _repository.SaveChangesAsync();

            await _elasticsearch.IndexPermissionAsync(permission);
            await _messaging.SendEventAsync(new PermissionEventDto { NameOperation = "modify" });

            return Unit.Value;
        }
    }
}
