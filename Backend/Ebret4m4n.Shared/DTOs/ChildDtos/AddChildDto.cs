using Ebret4m4n.Shared.CutomAttribute;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public record AddChildDto
{
    [Required(ErrorMessage = "يجب ادخال الرقم القومي للطفل")]
    [NationalId]
    public string Id { get; init; } = null!;

    [Required(ErrorMessage = "يجب ادخال اسم للطفل")]
    public string Name { get; init; } = null!;

    [Required(ErrorMessage = "يجب ادخال تاريخ ميلاد الطفل")]
    public DateTime BirthDate { get; init; }

    [Required(ErrorMessage = "يجب ادخال وزن للطفل")]
    [Range(1, 15, ErrorMessage = "يجب ان يكون وزن الطفل اكثر من 1كجم")]
    public double Weight { get; init; }

    [Required(ErrorMessage = "يجب ادخال جنس الطفل")]
    [AllowedValues(['F', 'f', 'm', 'M'], ErrorMessage = "يجب ان يكون جنس الطفل ذكر او انثي")]
    public char Gender { get; init; }

    public string? PatientHistory { get; init; }

    public List<IFormFile>? ReportFiles { get; init; }

    public List<string>? TakedVaccines { get; init; }
}
