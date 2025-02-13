namespace Ebret4m4n.Entities.Interfaces;
//public enum DayOfWeek { }//ايام الاسبوع
public interface IHealthCareCenter
{
    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; }
    public string FirstDay { get; set; }

    public string SecondDay { get; set; }

    public string HealthCareLocation { get; set; }

    
}
