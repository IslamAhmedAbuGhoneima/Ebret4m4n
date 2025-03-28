namespace Ebret4m4n.Shared.DTOs.ComplaintDtos;

public record ComplaintDto(
    Guid Id,
    string Message,
    string UserName,
    string Email,
    string PhoneNumber,
    string HealthCareCenterName,
    string FirstDay,
    string SecondDay,
    string HCLocation);