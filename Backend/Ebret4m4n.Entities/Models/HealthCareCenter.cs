using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

[Index(nameof(Name), Name = "IX_HealthCareCenter_Name")]
public class HealthCareCenter
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    [ForeignKey("Doctor")]
    public string DoctorId { get; set; } = null!;
    public Doctor? Doctor { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }
    public ApplicationUser? User {  get; set; }

    [ForeignKey("AdminOfHC")]
    public string AdminOfHCId { get; set; } = null!;
    public AdminOfHC? AdminOfHC { get; set; }
}
