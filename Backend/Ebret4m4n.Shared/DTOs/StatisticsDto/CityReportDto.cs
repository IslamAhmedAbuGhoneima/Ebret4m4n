using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs.StatisticsDto
{
	public class CityReportDto
	{
		public string City { get; set; }
		public int HealthUnitCount { get; set; }
		public int VaccinesTaken { get; set; }
	}
}
