namespace Ebret4m4n.Shared.DTOs.JobApplicationsDtos;

public record MedicalPositionRequestsDto(string ApplicantName, 
    string ApplicantLocation, 
    string MedicalNumber,
    string HealthCareName,
    string HealthCareGovernorate,
    string HealthCareCity,
    string HealthCareVillage);
