using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class HealthReportFile
{
    public string FilePath { get; set; } = null!;

    public DateTime UploadedOn { get; set; }

    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!; 
    public Child Child { get; set; }

}
