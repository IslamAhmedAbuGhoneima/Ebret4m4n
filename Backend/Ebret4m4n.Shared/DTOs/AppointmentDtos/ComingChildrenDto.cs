namespace Ebret4m4n.Shared.DTOs.AppointmentDtos;

public record ComingChildrenDto(Guid AppointmentId, string ChildId, string ChildName, string ParentName, string VaccineName);