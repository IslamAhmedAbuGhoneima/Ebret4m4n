namespace Ebret4m4n.Shared.DTOs.MedicalStaffDtos;

public record UpdateMedicalStaffDto(string FirstName, string LastName, string Email, Guid HealthCareCenterId);
