using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class CityAdminStaff 
{
    [Key, ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    [MaxLength(150)]
    public string Governorate { get; set; } = null!;

    [MaxLength(150)]
    public string City { get; set; } = null!;

    [ForeignKey("GovernorateAdminStaff")]
    public string? GovernorateAdminId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public GovernorateAdminStaff? GovernorateAdminStaff { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<MainInventory>? MainInventories { get; set; }
}
