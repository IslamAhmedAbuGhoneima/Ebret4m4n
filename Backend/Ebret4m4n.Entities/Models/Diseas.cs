using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Diseas
{
    #region Properties
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Severity { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; } 
    #endregion
}
