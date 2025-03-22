namespace Ebret4m4n.Shared.DTOs.OrderDtos;

public record OrderDto(Guid Id,string Antigen, uint Amount, string Status,DateTime DateRequested);
