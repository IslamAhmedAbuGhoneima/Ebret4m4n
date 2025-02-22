using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs
{
    public class UpdateChildDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Range(1, 15)]
        public double Weight { get; set; } 

        public string? PatientHistory { get; set; }
        public List<IFormFile> imageFiles { get; set; }
        public List<string> deleteImagePaths { get; set; }
    }
}
