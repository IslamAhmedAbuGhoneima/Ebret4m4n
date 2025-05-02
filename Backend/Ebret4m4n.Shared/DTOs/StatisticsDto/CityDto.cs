using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs.StatisticsDto
{
	public class CityDto
	{
		public int HealthCareUnits { get; set; }
		public int RegisteredChildren { get; set; }
		public int FullyVaccinatedChildren { get; set; }
		public double TotalVaccinesTaken { get; set; }
		public int TotalComplaints { get; set; }
		public int MaleChildren { get; set; }
		public int FemaleChildren { get; set; }
		public List<UnitReportDto> UnitsReport { get; set; }
		public List<UnitVaccineRequestDto> TopUnitsByVaccineRequests { get; set; }
	}
}
