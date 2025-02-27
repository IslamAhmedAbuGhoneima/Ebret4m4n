namespace Ebret4m4n.Shared.DTOs.VaccinDto;

public record ChildVaccinDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int DocesRequired { get; set; }

    public int DocesTaken { get; set; }

    public bool IsTaken { get; set; }

    public int ChildAge { get; set; }
}
