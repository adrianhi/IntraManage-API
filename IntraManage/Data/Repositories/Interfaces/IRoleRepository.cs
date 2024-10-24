using IntraManage.Data.Models;

namespace IntraManage.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);

    }
}
