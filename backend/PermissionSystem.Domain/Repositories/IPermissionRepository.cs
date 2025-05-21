using PermissionSystem.Domain.Entities;

namespace PermissionSystem.Domain.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetByIdAsync(int id);
        Task<List<Permission>> GetAllAsync();
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task SaveChangesAsync();
    }
}
