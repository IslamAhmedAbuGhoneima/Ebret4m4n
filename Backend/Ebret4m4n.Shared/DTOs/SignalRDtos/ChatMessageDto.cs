using Microsoft.AspNetCore.Http;

namespace Ebret4m4n.Shared.DTOs.SignalRDtos;

public record ChatMessageDto(string? Message, string SenderId, string ReceiverId, DateTime? SentAt)
{
    public IFormFile? File { get; set; }
}
