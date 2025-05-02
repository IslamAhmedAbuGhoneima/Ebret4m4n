using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs.StatisticsDto
{
	public class GovernorateReportDto
	{
		public string Governorate { get; set; }
		public int CityCount { get; set; }
		public int HealthUnitCount { get; set; }
		public int VaccinesTaken { get; set; }
	}
}
