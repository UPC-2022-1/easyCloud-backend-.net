using System.ComponentModel.DataAnnotations;

namespace easyCloud.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}