using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Diseas
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Severity { get; set; } = null!;

    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; }
}
