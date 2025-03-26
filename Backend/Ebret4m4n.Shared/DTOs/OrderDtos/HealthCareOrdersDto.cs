using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Shared.DTOs.OrderDtos;

public record HealthCareOrdersDto(int Id, string HealthCareCenterName, string Status, DateTime DateRequested);