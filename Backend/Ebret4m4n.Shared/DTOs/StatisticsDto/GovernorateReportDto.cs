namespace Ebret4m4n.Shared.DTOs.StatisticsDto;

public class GovernorateReportDto
{
	public string Governorate { get; set; } = string.Empty;
	public int CityCount { get; set; }
	public int HealthUnitCount { get; set; }
	public int VaccinesTaken { get; set; }
}
