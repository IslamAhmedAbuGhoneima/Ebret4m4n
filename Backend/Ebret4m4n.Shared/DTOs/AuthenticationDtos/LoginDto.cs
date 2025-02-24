using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record LoginDto
{
    [Required(ErrorMessage = "يجب ادخال عنوان البريد الالكتروني")]
    [EmailAddress(ErrorMessage = "هذا البريد الالكتروني غير صالح")]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "يجب ادخال كلمه سر")]
    public string Password { get; init; } = null!;
}
