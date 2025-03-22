using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

[PrimaryKey("InventoryLocation", "Antigen")]
public class MainInventory
{
    [MaxLength(150)]
    public string InventoryLocation { get; set; } = null!;

    [MaxLength(50)]
    public string Antigen { get; set; } = null!;

    public uint Amount { get; set; }

    [ForeignKey("CityAdminStaff")]
    public string? CityAdminStaffId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public CityAdminStaff? CityAdminStaff { get; set; }

    [ForeignKey("GovernorateAdminStaff")]
    public string? GovernorateAdminStaffId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public GovernorateAdminStaff? GovernorateAdminStaff { get; set; }
}
