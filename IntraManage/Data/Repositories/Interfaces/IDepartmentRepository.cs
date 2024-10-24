using IntraManage.Data.DTOs;

namespace IntraManage.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();
        Task<DepartmentDto> GetDepartmentById(int id);
    }
}
