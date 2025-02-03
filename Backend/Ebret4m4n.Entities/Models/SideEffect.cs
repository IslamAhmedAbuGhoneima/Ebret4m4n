using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class SideEffect
{
    public Guid Id { get; set; }

    public List<string> Description { get; set; } = null!;

    [ForeignKey("Vaccine")]
    public Guid VaccineId { get; set; }
    public Vaccine Vaccine { get; set; }
}
