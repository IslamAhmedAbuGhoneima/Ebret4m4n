namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public record ChildDataDto(string Id, string Name, int AgeInMonth, DateTime BirthDate, double Weight, char Gender, string? PatientHistory, List<string>? FilePath);