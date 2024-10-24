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
        public async Task<ActionResult<IEnumerable<Role>>> GetDepartments()
        {
            var payload = await _departmentRepository.GetAllDepartments();
            ApiResponse<IEnumerable<Department>> res = new();

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetDepartment(int id)
        {
            var payload = await _departmentRepository.GetDepartmentById(id);
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
