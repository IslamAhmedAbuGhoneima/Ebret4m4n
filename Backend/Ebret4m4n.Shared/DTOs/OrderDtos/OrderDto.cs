using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Shared.DTOs.OrderDtos;

public record OrderDto(string Antigen, uint Amount, string Status);
