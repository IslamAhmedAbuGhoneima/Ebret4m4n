namespace Ebret4m4n.Shared.DTOs.StatisticsDto;

public class AdminDashboardDto
{
    public int TotalHealthUnits { get; set; }
    public int RegisteredChildren { get; set; }
    public int FullyVaccinatedChildren { get; set; }
    public int TotalVaccinesTaken { get; set; }
    public int TotalVisits { get; set; }
    public int MalePercentage { get; set; }
    public int FemalePercentage { get; set; }
    public List<UnitVaccineStatsDto> TopUnits { get; set; } = new();
    public List<GovernorateStatsDto> GovernorateStats { get; set; } = new();
    public List<VaccineTypeStatsDto> VaccineTypeStats { get; set; } = new();
} 