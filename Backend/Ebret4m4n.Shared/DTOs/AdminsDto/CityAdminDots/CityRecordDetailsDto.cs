using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record CityRecordDetailsDto(string FirstName, string LastName, string Email, int HealthCareCount, List<InventoryDto> MainInventories, List<HealthCaresListDto> HealthCares);