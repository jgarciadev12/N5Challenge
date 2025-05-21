using MediatR;
using PermissionSystem.Application.Permissions.Dtos;
using PermissionSystem.Domain.Repositories;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionTypesQueryHandler : IRequestHandler<GetPermissionTypesQuery, List<PermissionTypeDto>>
    {
        private readonly IPermissionTypeRepository _repository;

        public GetPermissionTypesQueryHandler(IPermissionTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PermissionTypeDto>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetAllAsync();

            return types.Select(t => new PermissionTypeDto
            {
                Id = t.Id,
                Description = t.Description
            }).ToList();
        }
    }
}
