using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Notification
{
    #region Properties
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public ApplicationUser? User { get; set; } 
    #endregion
}
