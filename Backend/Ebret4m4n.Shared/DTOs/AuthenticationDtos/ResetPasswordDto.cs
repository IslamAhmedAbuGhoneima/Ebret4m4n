namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ResetPasswordDto(string UserId, string Token, string NewPassword);
