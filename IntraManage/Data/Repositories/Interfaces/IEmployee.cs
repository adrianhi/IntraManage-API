using IntraManage.Data.DTOs;

namespace IntraManage.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<GetEmployeeDto>> GetAllEmployees ( );    
        Task<GetEmployeeDto> GetEmployeeById (int id);             
        Task<GetEmployeeDto> CreateEmployee (PostEmployeeDto employee);  
        Task<LoginResponseDto> Authenticate (LoginRequestDto loginDto);


    }
}
