namespace Ebret4m4n.Entities.Models;


public class Inventory 
{
    #region Properties

    public Guid Id { get; set; }

    public uint Amount { get; set; }

    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public string HealthCareCenterGovernorate { get; set; } = null!;

    public string? HealthCareCenterCity { get; set; }

    public string? HealthCareCenterVillage { get; set; }

    public DayOfWeek FirstDay { get; set; }

    public DayOfWeek SecondDay { get ; set; }

    #endregion


    #region Relations
    public ICollection<Vaccine> Vaccines { get; set; } = [];
    #endregion
}
