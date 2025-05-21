using PermissionSystem.Domain.Events;

namespace PermissionSystem.Domain.Services
{
    public interface IMessagingService
    {
        Task SendEventAsync(PermissionEventDto eventDto);
    }
}
