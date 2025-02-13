using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

//[Index(nameof(Name), Name = "IX_Diseas_Name")]
//[Index(nameof(ChildId), Name = "IX_Diseas_ChildId")]
public class Diseas
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Severity { get; set; } = null!;

    [ForeignKey("Child")]
    public Guid ChildId { get; set; }
    public Child Child { get; set; }
}
