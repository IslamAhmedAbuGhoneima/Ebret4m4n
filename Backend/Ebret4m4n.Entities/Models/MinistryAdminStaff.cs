using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
	public class MinistryAdminStaff
	{
		[Key, ForeignKey("User")]
		public string UserId { get; set; } = null!;
		public ApplicationUser User { get; set; }

		public string Ministry { get; set; } ="Ministry Of Health";


		public ICollection<GovernorateAdminStaff> governorateAdminStaffs { get; set; } = [];

	}
}
