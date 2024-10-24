using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntraManage.Data.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IntraManageContext _context;
        public DepartmentRepository(IntraManageContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department is null)
            {
                return null;
            }
            else
            {
                return department;
            }
        }
    }
}
