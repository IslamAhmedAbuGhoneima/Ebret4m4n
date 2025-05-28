namespace Ebret4m4n.Shared.DTOs.StatisticsDto;

public class StatisticssDto
{
	public int TotalComplaints { get; set; }
	public double TotalVaccinesTaken { get; set; }
	public int FullyVaccinatedChildren { get; set; }
	public int RegisteredChildren { get; set; }
	public int HealthCareUnits { get; set; }
	public int MaleChildren { get; set; }
	public int FemaleChildren { get; set; }
	public double MalePercentage { get; set; }
	public double FemalePercentage { get; set; }

	public List<UnitVaccineRequestDto> TopHealthCareUnitsByVaccines { get; set; } = new();
	public List<UnitReportDto> AllUnits { get; set; } = new();
	public List<CityVaccineRequestDto> TopCitiesByVaccines { get; set; } = new();
	public List<CityReportDto> AllCities { get; set; } = new();
}
