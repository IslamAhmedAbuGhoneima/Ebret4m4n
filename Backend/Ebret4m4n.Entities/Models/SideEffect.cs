using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class SideEffect
{
    #region Properties
    public Guid Id { get; set; }

    public List<string> Description { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; }
    public Vaccine Vaccine { get; set; } 
    #endregion
}
