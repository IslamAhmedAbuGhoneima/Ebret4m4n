using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.MinistryOfHealthAdminDtos;

public record HealthCaresCityDto(CityRecordDetailsDto CityRecordDetails, List<HealthCaresListDto> HealthCaresList);