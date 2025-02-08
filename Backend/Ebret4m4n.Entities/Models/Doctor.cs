
using Ebret4m4n.Entities.Interfaces;

namespace Ebret4m4n.Entities.Models;

public class Doctor : ApplicationUser, IHealthCareCenter
{
    public int DoctorNumber { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public string HealthCareLocation { get; set; } = null!;
}
