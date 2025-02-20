
using Ebret4m4n.Entities.Interfaces;

namespace Ebret4m4n.Entities.Models;

public class Doctor : ApplicationUser, IHealthCareCenter
{
    #region Properties
    public int DoctorNumber { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }  
    #endregion
}
