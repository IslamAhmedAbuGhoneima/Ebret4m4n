using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs;

public record ForgotPasswordDto
{
    [Required(ErrorMessage ="Email address must not be null"), EmailAddress]
    public string Email { get; set; } = null!;
}
