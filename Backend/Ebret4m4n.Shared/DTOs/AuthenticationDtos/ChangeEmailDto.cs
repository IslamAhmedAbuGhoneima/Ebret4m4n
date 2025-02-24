using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ChangeEmailDto(string UserId, [EmailAddress] string NewEmail, string Token);
