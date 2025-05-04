namespace Ebret4m4n.Shared.DTOs.StatisticsDto;

public class AdminDto
{
	public int HealthCareUnits { get; set; }
	public int RegisteredChildren { get; set; }
	public int FullyVaccinatedChildren { get; set; }
	public double TotalVaccinesTaken { get; set; }
	public int TotalComplaints { get; set; }
	public int MaleChildren { get; set; }
	public int FemaleChildren { get; set; }
	public List<GovernorateVaccineRequestDto> TopGovernoratesByVaccines { get; set; }
	public List<GovernorateReportDto> GovernoratesReport { get; set; }
	public List<VaccineRequestDto> VaccineRequests { get; set; }
}
