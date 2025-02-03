using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ebret4m4n.Entities.Models;


[Index(nameof(UserId), Name = "IX_Certificate_UserId")]
[Index(nameof(ChildId), Name = "IX_Certificate_ChildId")]
public class Certificate
{
    public Guid Id { get; set; }

    public DateTime date { get; set; }

    [ForeignKey("Child")]
    public Guid ChildId { get; set; }
    public Child Child { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCarerCenterId { get; set; }
    public HealthCareCenter HealthCareCenter { get; set; }
}
