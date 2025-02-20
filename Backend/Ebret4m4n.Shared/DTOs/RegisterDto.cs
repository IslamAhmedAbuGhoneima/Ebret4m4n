using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs;

public record RegisterDto
{
    [Required(ErrorMessage ="FirstName Required")]
    [Length(3,20, ErrorMessage = "FirstName Should be between 3 and 20")] 
    public string FirstName { get; init; } = null!;

    [Required(ErrorMessage = "LastName Required")]
    [Length(3, 20,ErrorMessage ="LastName Should be between 3 and 20")]
    public string LastName { get; init; } = null!;

    [Required(ErrorMessage = "Email Required")]
    [EmailAddress(ErrorMessage ="Write a valid email")]
    public string Email { get; init; } = null !;

    [Required(ErrorMessage ="Password must not be null")]
    [MinLength(8,ErrorMessage ="Password at least length 8")]
    public string Password { get; init; } = null!;

    [RegularExpression(@"^0(10|11|12)[0-9]{8}")]
    [MinLength(11,ErrorMessage ="PhoneNumber must be at least 11 number")]
    public string PhoneNumber { get; init; } = null!;

    [Required(ErrorMessage = "Must Enter Governorate")]
    public string Governorate { get; init; } = null!;

    public string? City { get; init; }

    public string? Village { get; init; }
}
