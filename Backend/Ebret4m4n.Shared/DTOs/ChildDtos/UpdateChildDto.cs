using Ebret4m4n.Shared.CutomAttribute;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.ChildDtos;

public class UpdateChildDto
{
    [Required(ErrorMessage = "يجب ادخال رقم الطفل القومي")]
    [NationalId(ErrorMessage = "رقم الطفل القومي غير صحيح")]
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "يجب ادخال اسم الطفل")]
    public string Name { get; set; } = null!;

    [Range(1, 15, ErrorMessage = "يجب ادخال وزن الطفل اكثر من 1كجم واقل من 15 كجم")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "يجب ادخال سن الطفل")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "يجب ادخال جنس الطفل")]
    public char Gender { get; set; }

    public string? PatientHistory { get; set; }

    public List<IFormFile>? ImageFiles { get; set; }
}
