using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.JobApplicationsDtos;

public record JobApplicationDto(string MedicalNumber, [AllowedValues(["doctor","nurse"])] string Position);
