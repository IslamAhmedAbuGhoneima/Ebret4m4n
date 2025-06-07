namespace Ebret4m4n.Shared.DTOs.SignalRDtos;

public record ChatMessageDetailsDto(Guid Id, string? Message, string SenderId, string ReceiverId, bool IsRead, DateTime? SentAt, string? File);