using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Child
{
    #region Properties
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public int AgeInMonth { get; private set; } //=> (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 30);

    public double Weight { get; set; }

    public char Gender { get; set; }
    #endregion

    #region Relations
    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    public ICollection<Vaccine>? Vaccines { get; set; } = [];

    public ICollection<Diseas>? Diseas { get; set; } = [];

    public ICollection<HealthReportFile>? HealthReportFiles { get; set; } = []; 
    #endregion
}
