namespace Ebret4m4n.Shared.DTOs.AdminsDto.GovernorateAdminDtos;

public record SendVaccinRequestDto
{
    public string CityName { get; init; } = null!;

    public string VaccineName { get; init; } = null!;

    public int Amount { get; init; }
}
