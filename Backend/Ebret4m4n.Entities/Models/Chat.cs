using System.ComponentModel.DataAnnotations.Schema;


namespace Ebret4m4n.Entities.Models;

public class Chat
{
    #region Properties
    public Guid Id { get; set; }

    // new
    public DateTime DateTime => DateTime.Now;

    public string Message { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("MedicalStaff")]
    public string MedicalStaffId { get; set; } = null!;
    public MedicalStaff MedicalStaff { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } 
    #endregion
}
