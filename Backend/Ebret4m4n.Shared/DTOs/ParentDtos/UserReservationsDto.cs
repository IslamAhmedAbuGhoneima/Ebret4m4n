namespace Ebret4m4n.Shared.DTOs.ParentDtos;

public record UserReservationDto(Guid Id, string ChildName, string VaccineName, string Day, int RestOfDaysToAppointment);