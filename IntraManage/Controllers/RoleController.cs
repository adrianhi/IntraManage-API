using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using IntraManage.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        var payload = await _roleRepository.GetAllRoles();
        ApiResponse<IEnumerable<RoleDto>> res = new();

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

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetRole(int id)
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
