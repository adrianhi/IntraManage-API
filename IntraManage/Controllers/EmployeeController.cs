using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> GetEmployees ( )
        {
            var employeeDtos = await _employeeRepository.GetAllEmployees();

            if (employeeDtos == null || !employeeDtos.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<GetEmployeeDto>>
                {
                    code = 404,
                    hasError = true,
                    payload = null
                });
            }

            return Ok(new ApiResponse<IEnumerable<GetEmployeeDto>>
            {
                code = 200,
                hasError = false,
                payload = employeeDtos
            });
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

        [HttpPost("addEmploye")]
        public async Task<ActionResult<PostEmployeeDto>> AddEmployee (PostEmployeeDto employee)
        {

            try
            {
                var createdEmployee = await _employeeRepository.CreateEmployee(employee);
                ApiResponse<GetEmployeeDto> res = new()
                {
                    code = 200,
                    hasError = false,
                    payload = createdEmployee
                };
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                ApiResponse<GetEmployeeDto> res = new()
                {
                    code = 400,
                    hasError = true,
                    payload = null
                };
                return BadRequest(res);
            }
        }

        }
    }

