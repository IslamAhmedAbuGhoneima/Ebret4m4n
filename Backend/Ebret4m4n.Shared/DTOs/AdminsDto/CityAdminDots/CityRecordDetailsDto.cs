using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record CityRecordDetailsDto(string FirstName, string LastName, List<InventoryDto> MainInventories);

