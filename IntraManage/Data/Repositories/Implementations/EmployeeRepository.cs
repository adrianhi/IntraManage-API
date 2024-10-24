using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using IntraManage.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IntraManage.Data.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IntraManageContext _context;
        private readonly IConfiguration configuration;
        public EmployeeRepository (IntraManageContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        public async Task<LoginResponseDto> Authenticate (LoginRequestDto loginDto)
        {
            // Fetch the employee based on email
            var employee = await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Department)
                .SingleOrDefaultAsync(e => e.Email == loginDto.Email);

            if (employee == null || !BcryptUtils.VerifyPassword(loginDto.Password, employee.PasswordHash))
                return null;

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, employee.Email),
        new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
        new Claim(ClaimTypes.Role, employee.Role?.RoleName ?? "User")
    };

            string tokenValue = JwtUtils.GenerateJwtToken(
                configuration["Jwt:Key"],
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                60
            );

            return new LoginResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Token = tokenValue,
                RoleName = employee.Role?.RoleName ?? "basico",
                DepartmentName = employee.Department?.DepartmentName ?? "N/A"
            };
        }




        public async Task<GetEmployeeDto> CreateEmployee (PostEmployeeDto employeeDto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(employeeDto);

            if (!Validator.TryValidateObject(employeeDto, validationContext, validationResults, true))
            {
                throw new ArgumentException("Invalid employee data: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
            }

            if (await _context.Employees.AnyAsync(e => e.Email == employeeDto.Email))
            {
                throw new ArgumentException("An employee with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(employeeDto.PasswordHash);

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PasswordHash = hashedPassword,
                Cedula = employeeDto.Cedula,
                RoleId = employeeDto.RoleId,
                DepartmentId = employeeDto.DepartmentId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Fetch the newly created employee with the role and department names
            var role = await _context.Roles.FindAsync(employee.RoleId);
            var department = await _context.Departments.FindAsync(employee.DepartmentId);

            var employeeDtoToReturn = new GetEmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Cedula = employee.Cedula,
                RoleName = role?.RoleName, 
                DepartmentName = department?.DepartmentName 
            };

            return employeeDtoToReturn;
        }



        public async Task<GetEmployeeDto> GetEmployeeById (int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Role) // Include related Role
                .Include(e => e.Department) // Include related Department
                .SingleOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return null;

            return new GetEmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Cedula = employee.Cedula,
                RoleName = employee.Role != null ? employee.Role.RoleName : null,
                DepartmentName = employee.Department != null ? employee.Department.DepartmentName : null
            };
        }

        // Method to get all employees
        public async Task<IEnumerable<GetEmployeeDto>> GetAllEmployees ( )
        {
            return await _context.Employees
                .Include(e => e.Role) 
                .Include(e => e.Department) 
                .Select(e => new GetEmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Cedula = e.Cedula,
                    RoleName = e.Role != null ? e.Role.RoleName : null,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : null
                })
                .ToListAsync();
        }
    }
}
