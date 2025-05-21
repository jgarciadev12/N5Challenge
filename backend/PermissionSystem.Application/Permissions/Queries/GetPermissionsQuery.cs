using MediatR;
using PermissionSystem.Application.Permissions.Dtos;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionsQuery : IRequest<List<PermissionDto>>
    {
    }
}
