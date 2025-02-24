using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ResetEmailDto
{
    [EmailAddress, Required(ErrorMessage = "من فضلك ادخل عنوان بريد الكتروني")]
    public string NewEmail { get; init; } = null!;
}
