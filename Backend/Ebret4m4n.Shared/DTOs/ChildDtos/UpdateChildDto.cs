using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public class UpdateChildDto
{
    [Required(ErrorMessage = "يجب ادخال اسم الطفل")]
    public string Name { get; set; } = null!;

    [Range(1, 15, ErrorMessage = "يجب ادخال وزن الطفل اكثر من 1كجم واقل من 15 كجم")]
    public double Weight { get; set; }

    public string? PatientHistory { get; set; }

    public List<IFormFile>? ImageFiles { get; set; }
}
