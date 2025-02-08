using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

[Index(nameof(Email),Name = "IX_ApplicationUser_Email")]
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    [ForeignKey("HealthCareCenter")]
    public Guid HealthCareCenterId { get; set; }
    public HealthCareCenter? HealthCareCenter { get; set; }

    public ICollection<Child>? Children { get; set; } = [];

    public ICollection<Certificate>? Certificates { get; set; } = [];

    public ICollection<Notification>? Notifications { get; set; } = [];

    // new
    public ICollection<Appointment>? Appointments { get; set;} = [];

}
