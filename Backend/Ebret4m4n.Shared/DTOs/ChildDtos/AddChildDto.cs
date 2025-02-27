using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public record AddChildDto
{
    [Required(ErrorMessage = "يجب ادخال الرقم القومي للطفل")]
    [MinLength(14, ErrorMessage = "يجب ان يكون الرقم القومي للطفل مكن من 14 رقم")]
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "يجب ادخال اسم للطفل")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "يجب ادخال تاريخ ميلاد الطفل")]
    public DateTime BirthDate { get; set; }

    [Required]
    [Range(1, 15, ErrorMessage = "يجب ان يكون وزن الطفل اكثر من 1كجم")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "يجب ادخال جنس الطفل")]
    [AllowedValues(['F', 'f', 'm', 'M'], ErrorMessage = "يجب ان يكون جنس الطفل ذكر او انثي")]
    public char Gender { get; set; }

    public string? PatientHistory { get; set; }

    public List<IFormFile>? healthReportFiles { get; set; }

    public List<string>? vaccines { get; set; }
}
