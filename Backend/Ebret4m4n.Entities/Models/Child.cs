using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

[Index(nameof(Name),Name = "IX_Child_Name")]
public class Child
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public int AgeInMonth => (int)(DateTime.Now.Subtract(BirthDate).TotalDays / 30);

    public double Weight { get; set; }

    public char Gender { get; set; }

    //DiseasesHistory

    //public List<IFormFile> DiseasRepors { get; set; }

    [ForeignKey("Certificate")]
    public Guid CertificateId { get; set; }
    public Certificate Certificate { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public ICollection<Vaccine>? Vaccines { get; set; } = [];

    public ICollection<Diseas>? Diseas { get; set; } = [];
}
