using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs;

public record ChangeEmailDto(string UserId,[EmailAddress]string NewEmail,string Token);
