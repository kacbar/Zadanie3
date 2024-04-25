using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Students;

public class CreateStudentDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

