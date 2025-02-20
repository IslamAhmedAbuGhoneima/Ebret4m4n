namespace Ebret4m4n.Shared.DTOs;

public record ResetPasswordDto
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string NewPassword { get; set; }
}
