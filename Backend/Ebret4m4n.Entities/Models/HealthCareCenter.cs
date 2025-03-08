namespace Ebret4m4n.Entities.Models;

public class HealthCareCenter 
{
    public HealthCareCenter()
    {
        HealthCareCenterId = Guid.NewGuid();
    }

    #region Properties
    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }

    public string Governorate { get; set; } = null!;

    public string? City { get; set; }

    public string? Village { get; set; }
    #endregion

    #region Relations
    public ICollection<ApplicationUser> Users { get; set; } = [];
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Certificate> Certificates { get; set; } = [];
    public ICollection<Inventory> Inventories { get; set; } = [];
    #endregion
}
