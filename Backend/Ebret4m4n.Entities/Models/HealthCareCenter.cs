
using Ebret4m4n.Entities.Interfaces;

namespace Ebret4m4n.Entities.Models;

public class HealthCareCenter : IHealthCareCenter
{
    public Guid HealthCareCenterId { get; set; }
    public string HealthCareCenterName { get; set; }=null!;

    public string FirstDay { get; set; } = null!;

    public string SecondDay { get; set; } = null!;

    public string HealthCareLocation { get; set; }= null!;

    #region new fields
    ICollection<ApplicationUser> Users { get; set; } = [];
    ICollection<Appointment> Appointments { get; set; } = [];
    ICollection<Certificate> Certificates { get; set; } = [];
    #endregion

}
