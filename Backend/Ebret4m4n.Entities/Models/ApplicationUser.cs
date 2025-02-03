using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class ApplicationUser : IdentityUser
{
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }

    [ForeignKey("HealthCare")]
    public int HealthCareId { get; set; }
    public HealthCareCenter HealthCare { get; set; }

    public ICollection<Child>? Children { get; set; } = [];
    public ICollection<Certificate>? Certificates { get; set; } = [];
}
