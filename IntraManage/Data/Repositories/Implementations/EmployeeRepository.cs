using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntraManage.Data.Repositories.Implementations
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly IntraManageContext _context;

        public EmployeeRepository(IntraManageContext context) { 
        _context= context;        
        }

        public Task AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null) {
                return null;
            }
            else
            {
                return employee;
            }
        }
    }
}
