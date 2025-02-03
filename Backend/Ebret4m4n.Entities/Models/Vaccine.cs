using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

[Index(nameof(Name),Name ="IX_Vacccine_Name")]
public class Vaccine
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int DocesRequired { get; set; }

    public int DocesTaken { get; set; }

    public bool IsTaken { get; set; }

    public int ChildAge { get; set; }

    public bool IsDefult { get; set; }

    [ForeignKey("Child")]
    public Guid ChildId { get; set; }
    public Child Child {  get; set; }

    public ICollection<SideEffect> SideEffects { get; set; } = [];
    
}
