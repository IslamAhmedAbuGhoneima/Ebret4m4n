

using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs
{
    public class AddChildDto
    {
        [Required]
        [MinLength(14)]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [Range(1,15)]
        public double Weight { get; set; }
        [Required]
        [AllowedValues(['F','f','m',"M"])]
        public char Gender { get; set; }
        public string ParentId { get; set; } = null!;
        public string? PatientHistory { get; set; }
        public ICollection<IFormFile>? healthReportFiles { get; set; }

        public ICollection<ChildVaccineDto>? vaccines { get; set; } 
    }
}
