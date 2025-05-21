using Microsoft.EntityFrameworkCore;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Persistence;

namespace PermissionSystem.Infrastructure.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly PermissionDbContext _context;

        public PermissionTypeRepository(PermissionDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionType>> GetAllAsync()
        {
            return await _context.PermissionTypes.ToListAsync();
        }

        public async Task<PermissionType?> GetByIdAsync(int id)
        {
            return await _context.PermissionTypes.FindAsync(id);
        }
    }
}
