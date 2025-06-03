namespace Ebret4m4n.Shared.DTOs.NotificationDtos;

public record NotificatioDto(Guid Id, string Title, string Message, DateTime RecievedAt, bool IsRead);