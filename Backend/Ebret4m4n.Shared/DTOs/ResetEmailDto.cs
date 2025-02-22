using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs;

public record ResetEmailDto
{
    [EmailAddress,Required(ErrorMessage ="You should put email Address")]
    public string NewEmail { get; init; } = null!;
}
