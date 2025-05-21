using MediatR;
using PermissionSystem.Application.Permissions.Dtos;

namespace PermissionSystem.Application.Permissions.Queries
{
    public class GetPermissionByIdQuery : IRequest<PermissionDto?>
    {
        public int Id { get; set; }
    }
}
