using Ebret4m4n.Shared.DTOs.ChildDtos;

namespace Ebret4m4n.Shared.DTOs;

public record UserDataDto
{
    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Governorate { get; init; } = null!;

    public string? City { get; init; } 

    public string? PhoneNumber { get; init; }

    public string? Village { get; init; }

    public List<ChildDto>? Children { get; init; } = [];

}
