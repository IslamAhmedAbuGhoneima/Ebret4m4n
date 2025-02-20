namespace Ebret4m4n.Entities.Interfaces;

public interface IHealthCareCenter
{
    //public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; }

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get; set; }

    //public string HealthCareLocation { get; set; }

    public string Governorate { get; set; }

    public string City { get; set; }

    public string Village { get; set; }
 
}
