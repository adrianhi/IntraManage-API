using IntraManage.Data.DTOs;

namespace IntraManage.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleById(int id);

    }
}
