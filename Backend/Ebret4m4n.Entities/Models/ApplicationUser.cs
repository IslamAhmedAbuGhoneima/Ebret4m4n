using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

[Index(nameof(Email),Name = "IX_ApplicationUser_Email")]
public class ApplicationUser : IdentityUser
{
    #region Properties
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Governorate { get; set; } = null!;

    public string? City { get; set; }

    public string? Village { get; set; }

    #region User Tokens
    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }
    #endregion


    #endregion

    #region Relations
    [ForeignKey("HealthCareCenter")]
    public Guid? HealthCareCenterId { get; set; }
    public HealthCareCenter? HealthCareCenter { get; set; }

    public ICollection<Child>? Children { get; set; } = [];

    public ICollection<Certificate>? Certificates { get; set; } = [];

    public ICollection<Notification>? Notifications { get; set; } = [];

    public ICollection<Appointment>? Appointments { get; set; } = []; 
    #endregion
}
