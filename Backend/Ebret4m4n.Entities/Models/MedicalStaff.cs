using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public enum StaffRole
{
    Doctor,
    Organizer,
}

public class MedicalStaff 
{
    #region Properties

    [Key, ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    public string? HealthCareCenterName { get; set; }

    public string? HealthCareCenterGovernorate { get; set; }

    public string? HealthCareCenterCity { get; set; }

    public string? HealthCareCenterVillage { get; set; }

    public StaffRole StaffRole { get; set; } 

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }

    // key of HealthCare he work in
    public Guid? HCCenterId { get; set; }
	public ICollection<Order> Orders { get; set; }

    [ForeignKey("CityAdminStaff")]
    public string CityAdminStaffId { get; set; } = null!;
    public CityAdminStaff CityAdminStaff { get; set; }
	#endregion
}
