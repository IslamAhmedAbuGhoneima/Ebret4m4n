using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public enum OrderStatus : byte
{
    Pending,
    Processing,
    Recived
}

public class Order
{
    public Guid Id { get; set; }
	public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime DateRequested { get; private set; } = DateTime.UtcNow;
    public DateTime? DateApproved { get; set; }

    #region Relations

    [ForeignKey("MedicalStaff")]
    public string? MedicalStaffId { get; set; }
    public MedicalStaff? MedicalStaff { get; set; }

    [ForeignKey("CityAdminStaff")]
    public string? CityAdminStaffId { get; set; }
    public CityAdminStaff? CityAdminStaff { get; set; }

    [ForeignKey("GovernorateAdminStaff")]
    public string? GovernorateAdminStaffId { get; set; }
    public GovernorateAdminStaff? GovernorateAdminStaff { get; set; }

	public ICollection<OrderItem> OrderItems { get; set; } = [];
	#endregion
}
