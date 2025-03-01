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

    ICollection<CityAdminStaff> CityAdminStaffs { get; set; } = [];
}
