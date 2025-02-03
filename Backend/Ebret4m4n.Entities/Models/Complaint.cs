using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

[Index(nameof(UserId),Name = "IX_Complaint_UserId")]
public class Complaint
{
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }
    
}
