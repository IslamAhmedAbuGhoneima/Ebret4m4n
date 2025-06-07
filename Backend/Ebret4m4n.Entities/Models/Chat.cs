using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Chat
{
    #region Properties
    public Guid Id { get; set; }

    public DateTime SentAt { get; set; }

    public string? Message { get; set; } 

    // for uploadFiles
    public string? File { get; set; }
    public bool IsRead { get; set; } = false;
    #endregion

    #region Relations
    [ForeignKey("Receiver")]
    public string ReceiverId { get; set; } = null!;
    public ApplicationUser? Receiver { get; set; }

    [ForeignKey("Sender")]
    public string SenderId { get; set; } = null!;
    public ApplicationUser? Sender { get; set; }
    #endregion
}
