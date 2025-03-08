namespace Ebret4m4n.Shared.DTOs.ComplaintDtos;

public record ComplaintDto(string Message,
    string UserName,
    string Email,
    string PhoneNumber,
    string HealthCareCenterName,
    DayOfWeek FirstDay,
    DayOfWeek SecondDay,
    string HCLocation);