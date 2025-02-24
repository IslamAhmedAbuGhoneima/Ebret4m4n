namespace Ebret4m4n.Shared.DTOs.ParentDtos;

public record AppointmentDto
{
    public string ChildName { get; init; } = null!;

    public string Day { get; init; } = null!;

    public DateTime Date { get; init; }
}
