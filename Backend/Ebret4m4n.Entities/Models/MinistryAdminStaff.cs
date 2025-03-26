using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Entities.Models;

public class MinistryAdminStaff
{
	[Key, ForeignKey("User")]
	public string UserId { get; set; } = null!;
	public ApplicationUser User { get; set; }

	public ICollection<GovernorateAdminStaff> GovernorateAdminStaffs { get; set; } = [];
}
