using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;


public enum JopPosition
{
    Doctor,
    Nurse
}

public enum ApplicationStatus
{
    Pending,
    Accepted,
    Cancled
}

public class JobApplications
{
    public Guid JobId { get; set; }

    public string MedicalNumber { get; set; } = null!;

    public JopPosition JobPosition { get; set; } 

    public ApplicationStatus JobStatus { get; set; } = ApplicationStatus.Pending;

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }
}
