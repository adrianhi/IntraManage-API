using System.ComponentModel.DataAnnotations;

namespace IntraManage.Data.DTOs
{
    public class PostEmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]

        public string PasswordHash { get; set; } = null!;

        public IFormFile CedulaFile { get; set; } = null!;
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }

    }
}
