using PermissionSystem.Domain.Entities;

namespace PermissionSystem.Domain.Repositories
{
    public interface IPermissionTypeRepository
    {
        Task<List<PermissionType>> GetAllAsync();
        Task<PermissionType?> GetByIdAsync(int id);
    }
}
