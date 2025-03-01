using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;


public enum ApplicantPosition
{
    Doctor,
    Organizer,
}

public enum ApplicationStatus
{
    Pending,
    Accepted,
    Rejected
}

public class MedicalApplication
{
    public Guid ApplicationId { get; set; }

    public string MedicalNumber { get; set; } = null!;

    public ApplicantPosition ApplicantPosition  { get; set; } 

    public ApplicationStatus ApplicationStatus { get; set; } = ApplicationStatus.Pending;

    public string HealthCareName { get; set; } = null!;

    public string HealthCareGovernorate { get; set; } = null!;

    public string HealthCareCity { get; set; } = null!;

    public string HealthCareVillage { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }
}
