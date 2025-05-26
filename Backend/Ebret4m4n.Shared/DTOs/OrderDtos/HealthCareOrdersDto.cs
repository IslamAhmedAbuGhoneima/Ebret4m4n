namespace Ebret4m4n.Shared.DTOs.OrderDtos;

public record HealthCareOrdersDto(Guid Id, string HealthCareCenterName, string Status, DateTime DateRequested);