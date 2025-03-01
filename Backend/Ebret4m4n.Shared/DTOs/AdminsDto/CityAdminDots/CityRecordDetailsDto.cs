namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record CityRecordDetailsDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public List<string> VaccineInventory { get; set; } = [];
}
