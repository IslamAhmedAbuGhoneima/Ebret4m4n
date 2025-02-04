using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class HealthReportFile
{
    public string FilePath { get; set; } = null!;

    public DateTime UploadedOn => DateTime.Now;

    [ForeignKey("Child")]
    public Guid ChildId { get; set; }
    public Child Child { get; set; }

}
