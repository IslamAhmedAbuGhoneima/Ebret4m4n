using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.HealthCareDtos;

public record HealthCareDetailsDto(string HealthCareCenterName, string FirstDay, string SecondDay, string Governorate, string? City, string? Village)
{
    public string? OrganizerName { get; set; }

    public string? DoctorName { get; set; }

    public List<InventoryDto> Inventories { get; set; } = [];
}