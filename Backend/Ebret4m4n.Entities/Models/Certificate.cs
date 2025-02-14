using System.ComponentModel.DataAnnotations.Schema;
namespace Ebret4m4n.Entities.Models;


public class Certificate
{
    public Guid Id { get; set; }

    public DateTime date { get; set; }

    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; } 

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCarerCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; }
}
