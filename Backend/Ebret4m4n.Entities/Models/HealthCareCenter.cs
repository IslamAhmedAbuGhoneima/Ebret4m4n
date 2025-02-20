
using Ebret4m4n.Entities.Interfaces;

namespace Ebret4m4n.Entities.Models;

public class HealthCareCenter : IHealthCareCenter
{
    #region Properties
    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }

    //public string HealthCareLocation { get; set; } = null!;
    public string Governorate { get; set; } = null!;

    public string? City { get; set; }

    public string? Village { get; set; }
    #endregion

    #region Relations
    ICollection<ApplicationUser> Users { get; set; } = [];
    ICollection<Appointment> Appointments { get; set; } = [];
    ICollection<Certificate> Certificates { get; set; } = [];
    #endregion
}
