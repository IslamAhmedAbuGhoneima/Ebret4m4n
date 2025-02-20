using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Chat
{
    #region Properties
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } 
    #endregion
}
