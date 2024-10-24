namespace IntraManage.Data.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!; 
        public string RoleName { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;

    }
}
