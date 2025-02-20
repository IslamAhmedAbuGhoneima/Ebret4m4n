using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs;

public record LoginDto
{
    [Required(ErrorMessage = "Email must not be empty")]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "Please Enter password")]
    public string Password { get; init; } = null!;
}
