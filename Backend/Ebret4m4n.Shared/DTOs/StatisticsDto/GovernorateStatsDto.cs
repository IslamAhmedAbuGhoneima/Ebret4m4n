namespace Ebret4m4n.Shared.DTOs.StatisticsDto;

public class GovernorateStatsDto
{
    public string Governorate { get; set; } = string.Empty;
    public int HealthUnitsCount { get; set; }
    public int VaccinesTaken { get; set; }
    public List<UnitStatsDto> Units { get; set; } = new();
} 