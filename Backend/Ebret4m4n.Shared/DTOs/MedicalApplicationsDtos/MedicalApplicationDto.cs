using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.MedicalApplicationsDtos;

public record MedicalApplicationDto(
    string MedicalNumber, 
    [AllowedValues(["doctor","organizer"])] string Position,
    string HealthCareName,
    string HealthCareGovernorate,
    string HealthCareCity,
    string HealthCareVillage);
