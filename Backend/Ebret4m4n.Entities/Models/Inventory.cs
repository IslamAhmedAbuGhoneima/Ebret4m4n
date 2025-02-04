using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

[Index(nameof(VaccineId),Name = "IX_Inventory_VaccineId")]
[Index(nameof(HealthCareCenterId), Name = "IX_Inventory_HealthCareCenterId")]
public class Inventory
{
    public Guid Id { get; set; }

    public uint Amount { get; set; }

    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; }
    public Vaccine Vaccine { get; set; }

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; }
}
