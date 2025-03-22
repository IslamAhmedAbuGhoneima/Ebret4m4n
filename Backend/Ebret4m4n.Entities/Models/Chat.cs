using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Chat
{
    #region Properties
    public Guid Id { get; set; }

    public DateTime SentAt { get; private set; } = DateTime.UtcNow;

    public string Message { get; set; } = null!;
    #endregion

    #region Relations
    [ForeignKey("Receiver")]
    public string? ReceiverId { get; set; }
    public ApplicationUser? Receiver { get; set; }

    [ForeignKey("Sender")]
    public string? SenderId { get; set; } 
    public ApplicationUser? Sender { get; set; }
    #endregion
}
