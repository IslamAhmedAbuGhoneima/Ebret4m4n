namespace Ebret4m4n.Entities.Interfaces;

public interface IHealthCareCenter
{
    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; }

    public string HealthCareLocation { get; set; }
}
