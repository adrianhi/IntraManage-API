namespace IntraManage.Data.DTOs
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Cedula { get; set; } = null!;
        public string? RoleName { get; set; }

        public string? DepartmentName { get; set; }
    }
}
