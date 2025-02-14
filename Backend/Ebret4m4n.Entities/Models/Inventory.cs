using Ebret4m4n.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;


public class Inventory : IHealthCareCenter
{
    public Guid Id { get; set; }

    public uint Amount { get; set; }

    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; }
    public Vaccine Vaccine { get; set; }

    #region new fields

    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; } = null!;

    public string HealthCareLocation { get; set; } = null!;

    public WeekDays FirstDay { get; set; }

    public WeekDays SecondDay { get ; set; }

    #endregion

    
}
