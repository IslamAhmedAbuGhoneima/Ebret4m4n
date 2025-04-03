using Ebret4m4n.Shared.DTOs.InventoriesDtos;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.GovernorateAdminDtos;

public record GovernorateDetailsDto(string Governorate, string FirstName, string LastName, string Email, int CityCounts, int HealthCareCount, List<InventoryDto> MainInventories, List<string> Cities);
