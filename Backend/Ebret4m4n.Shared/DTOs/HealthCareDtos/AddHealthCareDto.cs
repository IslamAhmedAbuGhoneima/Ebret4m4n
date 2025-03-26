namespace Ebret4m4n.Shared.DTOs.HealthCareDtos;

public record AddHealthCareDto(
    string HealthCareCenterName,
    string FirstDay,
    string SecondDay,
    string Governorate,
    string? City,
    string? Village);
