using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

[Index(nameof(HealthCareCenterId),Name = "IX_HealthCareAppointment_HealthCareCenterId")]
public class HealthCareAppointment
{
    public Guid Id { get; set; }

    public string FirstDay { get; set; } = null!;

    public string SecondDay { get; set; } = null!;


    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; }
}
