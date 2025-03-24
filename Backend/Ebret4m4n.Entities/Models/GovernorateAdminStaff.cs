using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Entities.Models;

public class GovernorateAdminStaff
{
    [Key, ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    [MaxLength(150)]
    public string Governorate { get; set; } = null!;

    public ICollection<CityAdminStaff> CityAdminStaffs { get; set; } = [];
	public ICollection<Order> Orders { get; set; }


	public ICollection<MainInventory>? MainInventories { get; set; } = [];

}
