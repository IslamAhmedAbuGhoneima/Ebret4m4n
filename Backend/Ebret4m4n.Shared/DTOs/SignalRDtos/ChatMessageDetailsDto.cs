namespace Ebret4m4n.Shared.DTOs.SignalRDtos;

public record ChatMessageDetailsDto(string? Message, string SenderId, string ReceiverId, DateTime? SentAt, string? File);