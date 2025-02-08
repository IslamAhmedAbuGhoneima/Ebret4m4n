using Ebret4m4n.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

[Index(nameof(VaccineId),Name = "IX_Inventory_VaccineId")]
// [Index(nameof(HealthCareCenterId), Name = "IX_Inventory_HealthCareCenterId")]
public class Inventory : IHealthCareCenter
{
    public Guid Id { get; set; }

    public uint Amount { get; set; }

    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; }
    public Vaccine Vaccine { get; set; }

    #region new fields

    public Guid HealthCareCenterId { get; set; }

    public string HealthCareCenterName { get; set; }

    public string HealthCareLocation { get; set; }

    #endregion

    //Remove
    //[ForeignKey("HealthCareCenter")]
    //public Guid HealthCareCenterId { get; set; }
    //public HealthCareCenter HealthCareCenter { get; set; }
}
