namespace Ebret4m4n.Shared.DTOs.VaccinDto;

public record ChildVaccinDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public bool IsTaken { get; init; }

    public int ChildAge { get; init; }
}
