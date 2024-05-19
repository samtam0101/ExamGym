using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class RegisterDto
{
    public string Name { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
}
