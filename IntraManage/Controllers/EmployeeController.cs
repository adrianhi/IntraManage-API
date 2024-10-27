using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace IntraManage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly string _uploadsFolder;

        public EmployeeController (IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Cedulas");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }


        [Authorize]
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


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetEmployee (int id)
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

       // [Authorize]
        [HttpPost("addEmploye")]
        public async Task<IActionResult> CreateEmployee ([FromForm] PostEmployeeDto employeeDto)
        {
            try
            {
                var createdEmployee = await _employeeRepository.CreateEmployee(employeeDto);
                var response = new ApiResponse<GetEmployeeDto>
                {
                    code = 200,
                    hasError = false,
                    payload = createdEmployee
                };
                return Ok(response);
            }
            catch (ArgumentException)
            {
                var response = new ApiResponse<GetEmployeeDto>
                {
                    code = 400,
                    hasError = true,
                    payload = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                var response = new ApiResponse<GetEmployeeDto>
                {
                    code = 500,
                    hasError = true,
                    payload = null
                };
                return StatusCode(500, response);
            }
        }
      


        [Authorize]
        [HttpGet("file/{userId}")]
        public async Task<IActionResult> DownloadFile (int userId)
        {
            var employee = await _employeeRepository.GetEmployeeById(userId);
            if (employee == null)
            {
                return NotFound("User not found.");
            }

            var filePath = employee.Cedula;
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath); 
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}

