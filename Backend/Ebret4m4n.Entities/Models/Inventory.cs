using Ebret4m4n.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

//[Index(nameof(VaccineId),Name = "IX_Inventory_VaccineId")]
//[Index(nameof(Vaccine.Name),Name = "IX_Inventory_VaccineName")]
/// <summary>
/// /[Index(nameof(HealthCareCenterId), Name = "IX_Inventory_HealthCareCenterId")]
/// </summary>
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
    public string FirstDay { get; set; }
    public string SecondDay { get ; set; }

    #endregion

    
}
