using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class OrderItem
{
	public Guid Id { get; set; }

	public string Antigen { get; set; } = null!;

	public uint Amount { get; set; }

	[ForeignKey("Order")]
	public Guid OrderId { get; set; }
	public Order Order { get; set; }
}
