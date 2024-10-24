using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntraManage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepository;
        
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartments();

            if (departments == null)
            {
                return NotFound(new ApiResponse<IEnumerable<DepartmentDto>>
                {
                    code = 404,
                    hasError = true,
                    payload = null
                });
            }

            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName
            });

            return Ok(new ApiResponse<IEnumerable<DepartmentDto>>
            {
                code = 200,
                hasError = false,
                payload = departmentDtos
            });
        
    }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetDepartment (int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            ApiResponse<DepartmentDto> res = new();

            if (department is null)
            {
                res.code = 404;
                res.hasError = true;
                return NotFound(res);
            }

            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };

            res.code = 200;
            res.hasError = false;
            res.payload = departmentDto;
            return Ok(res);
        }

    }
}
