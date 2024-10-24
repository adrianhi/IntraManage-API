using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntraManage.Data.Repositories.Implementations


{
    public class RoleRepository : IRoleRepository
    {
        private readonly IntraManageContext _context;
        public RoleRepository( IntraManageContext context) { 
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role= await _context.Roles.FindAsync(id);

            if (role is null)
            {
                return null;
            }
            else
            {
                return role;
            }

        }

    }
}
