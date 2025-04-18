using System.ComponentModel.DataAnnotations.Schema;

namespace Ebret4m4n.Entities.Models;

public class Transaction
{
    public string? SessionId { get; set; } 

    public string? PaymentIntentId { get; set; }

    public long? Amount { get; set; }

    public bool Paid { get; set; } = false;

    public DateTime? PaidAt { get; set; }

    [ForeignKey("Child")]
    public string ChildId { get; set; } = null!;
    public Child Child { get; set; } = null!;

    [ForeignKey("Parent")]
    public string ParentId { get; set; } = null!;
    public ApplicationUser Parent { get; set; } = null!;
}
