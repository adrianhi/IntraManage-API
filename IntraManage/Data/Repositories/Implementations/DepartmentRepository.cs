using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using IntraManage.Data.DTOs;

namespace IntraManage.Data.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IntraManageContext _context;

        public DepartmentRepository (IntraManageContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments ( )
        {
            var departments = await _context.Departments.ToListAsync();
            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName
            });
        }

        public async Task<DepartmentDto> GetDepartmentById (int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department is null)
            {
                return null;
            }

            return new DepartmentDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }
    }
}
