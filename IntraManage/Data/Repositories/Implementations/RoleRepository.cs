using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntraManage.Data.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IntraManageContext _context;

        public RoleRepository (IntraManageContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles ( )
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }

        public async Task<RoleDto> GetRoleById (int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return null;
            }

            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
    }
}
