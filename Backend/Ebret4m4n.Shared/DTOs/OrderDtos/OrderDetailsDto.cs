using Ebret4m4n.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs.OrderDtos
{
	public class OrderDetailsDto
	{
		public string? HealthCareCenterName { get; set; }

		public string? HealthCareCenterGovernorate { get; set; }

		public string? HealthCareCenterCity { get; set; }

		public string? HealthCareCenterVillage { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

	}
}
