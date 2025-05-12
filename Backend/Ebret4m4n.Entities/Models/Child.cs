using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Child
{
    #region Properties
    
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public int AgeInMonth { get; private set; } 

    public double Weight { get; set; }

    public char Gender { get; set; }

    public bool IsNormal { get; set; } = true;

    public string? PatientHistory {  get; set; }
    
    #endregion

    #region Relations
    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    public Transaction Transaction { get; set; } = null!;

    public ICollection<Vaccine>? Vaccines { get; set; } = [];

    public ICollection<Appointment>? Appointments {  get; set; } = [];

    public ICollection<HealthReportFile>? HealthReportFiles { get; set; } = []; 
    #endregion
}
