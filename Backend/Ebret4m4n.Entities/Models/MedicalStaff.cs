using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class MedicalStaff 
{
    #region Properties

    [Key, ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }


    public string MedicalNumber { get; set; } = null!;

    public string? HealthCareCenterName { get; set; }

    public string? HealthCareCenterGovernorate { get; set; }

    public string? HealthCareCenterCity { get; set; }

    public string? HealthCareCenterVillage { get; set; }

    public string StaffRole { get; set; } = null!;

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }

    // key of HealthCare he work in
    public Guid? HCCenterId { get; set; }

    #endregion
}
