using IntraManage.Data.DTOs;
using IntraManage.Data.Models;
using IntraManage.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntraManage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public LoginController (IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult> LogIn (LoginRequestDto user)
        {
            var auth = await _employeeRepository.Authenticate(user);
            ApiResponse<LoginResponseDto> res = new();

            if (auth is null)
            {
                res.code = 404;
                res.hasError = true;
                return NotFound(res);
            }

            res.code = 200;
            res.hasError = false;
            res.payload = auth;
            return Ok(res);
        }
    }
}
