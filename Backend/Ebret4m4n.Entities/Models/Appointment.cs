using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Appointment
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Day => Date.ToString("dddd");

    public string Status { get; set; } = null!;  //Enum

    public string Location { get; set; } = null!;

    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; }
}