using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record CityRecordDetailsDto
{
    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public List<InventoryDto> MainInventories { get; init; } = [];
}
