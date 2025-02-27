using System.ComponentModel.DataAnnotations.Schema;
namespace Ebret4m4n.Entities.Models;

public class Appointment
{
    #region Properties
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Day => Date.ToString("dddd");

    public string Location { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    // new Relation
    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; } 
    public Vaccine Vaccine {  get; set; }

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; } 
    #endregion
}