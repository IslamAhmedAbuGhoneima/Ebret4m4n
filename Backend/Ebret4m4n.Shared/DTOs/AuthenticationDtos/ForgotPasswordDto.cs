using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ForgotPasswordDto
{
    [Required(ErrorMessage = "يجب ادخال عنوان البريد الالكتروني"),
        EmailAddress(ErrorMessage ="هذا البريد الالكتروني غير صالح")]
    public string Email { get; set; } = null!;
}
