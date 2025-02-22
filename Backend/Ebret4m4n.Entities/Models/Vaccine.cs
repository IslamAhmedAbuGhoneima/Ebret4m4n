using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Vaccine
{
    #region Properties
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int DocesRequired { get; set; }

    public int DocesTaken { get; set; }

    public bool IsTaken { get; set; }

    public int ChildAge { get; set; }
    #endregion

    #region Relations
    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; }

    public ICollection<SideEffect> SideEffects { get; set; } = []; 
    #endregion
}
