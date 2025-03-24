using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
	
	public class OrderItem
	{
		//public Guid Id { get; set; }
		public string Antigen { get; set; } = null!;

		public uint Amount { get; set; }

		[ForeignKey("Order")]
		public Guid orderId { get; set; }
		public Order Order { get; set; }
	}
}
