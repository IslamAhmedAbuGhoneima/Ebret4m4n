namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ResetPasswordDto
{
    public string UserId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string NewPassword { get; set; } = null!;
}
