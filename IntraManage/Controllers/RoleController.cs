using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces; 

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    // Constructor with dependency injection
    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // GET: api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        var payload = await _roleRepository.GetAllRoles();
        ApiResponse<IEnumerable<Role>> res = new();

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

    // GET: api/roles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
        var payload = await _roleRepository.GetRoleById(id);
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
