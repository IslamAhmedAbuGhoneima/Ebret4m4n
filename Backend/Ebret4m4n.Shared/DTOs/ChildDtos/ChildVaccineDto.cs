namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public record ChildVaccineDto
{
    public string Name { get; init; }

    public int ChildAge { get; init; }

    public bool IsTake { get; init; } = true;
}
