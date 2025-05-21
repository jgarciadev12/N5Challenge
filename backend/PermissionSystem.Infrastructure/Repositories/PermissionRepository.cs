using Microsoft.EntityFrameworkCore;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Persistence;

namespace PermissionSystem.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PermissionDbContext _context;

        public PermissionRepository(PermissionDbContext context)
        {
            _context = context;
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions.Include(p => p.PermissionType)
                                             .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _context.Permissions.Include(p => p.PermissionType).ToListAsync();
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
