using Ebret4m4n.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs.OrderDtos
{
	public class MyOrderDetailsDto
	{
		public OrderStatus Status { get; set; }
		public DateTime DateRequested { get;  set; } 
		public DateTime? DateApproved { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


	}
}
