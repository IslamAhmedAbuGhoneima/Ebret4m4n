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

    public string Antigen { get; set; } = null!;

    public uint Amount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime DateRequested { get; private set; } = DateTime.UtcNow;

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

    #endregion
}
