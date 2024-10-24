using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
namespace IntraManage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

 

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Employee>>>> GetEmployees()
        {
            var payload = await _employeeRepository.GetAllEmployees();
            ApiResponse<IEnumerable<Employee>> res = new();

            if (payload is null)
            {
                res.code = 404;
                res.hasError = true;
                return NotFound(res);
            }
            res.code = 200;
            res.hasError = false;
            res.payload = payload;
            return Ok(res);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetEmployee(int id)
        {
            var payload = await _employeeRepository.GetEmployeeById(id);
            ApiResponse<Object> res = new();

            if (payload is null)
            {
                res.code = 404;
                res.hasError = true;
                return NotFound(res);
            }
            res.code = 200;
            res.hasError = false;
            res.payload = payload;
            return Ok(res);
        }
    }
    }

