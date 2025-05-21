using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;

namespace PermissionSystem.Domain.Services
{
    public interface IElasticsearchService
    {
        Task IndexPermissionAsync(Permission permission);
        Task IndexPermissionEventAsync(PermissionEventDto dto);
    }
}
