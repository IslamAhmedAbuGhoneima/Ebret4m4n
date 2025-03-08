using Ebret4m4n.Shared.DTOs.VaccinDto;

namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public record ChildDto
{
    public string Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public int AgeInMonth { get; init; }

    public DateTime BirthDate { get; init; }

    public double Weight { get; set; }

    public char Gender { get; set; }

    public string? PatientHistory { get; set; }

    public List<ChildVaccinDto>? Vaccines { get; set; }

    public List<string>? FilePath { get; set; }
}