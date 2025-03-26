using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Shared.DTOs.OrderDtos;

public record MyOrderDetailsDto(Guid Id, string Status, DateTime DateRequested);
