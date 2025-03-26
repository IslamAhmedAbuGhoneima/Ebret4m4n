using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.HealthCareDtos;

public record HealthCareDetailsDto(
    )
{
    public string HealthCareCenterName { get; init; } = null!;

    public string? OrganizerName { get; set; }

    public string? DoctorName { get; set; }

    public DayOfWeek FirstDay { get; init; }

    public DayOfWeek SecondDay { get; init; }

    public string Governorate { get; init; } = null!;

    public string? City { get; init; }

    public string? Village { get; init; }

    public List<InventoryDto> Inventories { get; set; }
}
