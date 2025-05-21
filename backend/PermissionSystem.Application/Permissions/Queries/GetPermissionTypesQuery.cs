using MediatR;
using PermissionSystem.Application.Permissions.Dtos;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionTypesQuery : IRequest<List<PermissionTypeDto>>
    {
    }
}
