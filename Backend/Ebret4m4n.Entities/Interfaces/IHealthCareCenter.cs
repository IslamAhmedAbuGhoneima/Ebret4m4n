namespace Ebret4m4n.Entities.Interfaces;
public enum WeekDays
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Saturday
}
public interface IHealthCareCenter
{
    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; }

    public WeekDays FirstDay { get; set; }

    public WeekDays SecondDay { get; set; }

    public string HealthCareLocation { get; set; }
    
}
