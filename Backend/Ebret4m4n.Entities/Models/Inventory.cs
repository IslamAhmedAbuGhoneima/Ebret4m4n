using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Inventory 
{
    #region Properties
    public string Antigen { get; set; } = null!;

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }

    public HealthCareCenter HealthCareCenter { get; set; }

    public uint Amount { get; set; }
    #endregion
}
