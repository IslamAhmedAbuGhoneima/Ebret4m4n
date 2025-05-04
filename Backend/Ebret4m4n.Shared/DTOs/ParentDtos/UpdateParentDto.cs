using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.ParentDtos;

public record UpdateParentDto(
    [Required(ErrorMessage = "يجب ادخال الاسم الاول")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    string FirstName,
    [Required(ErrorMessage = "يجب ادخال اسم العائله")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    string LastName,
    [RegularExpression(@"^(01)(0|1|2)[0-9]{8}")]
    [MinLength(11, ErrorMessage = "من فضلك ادخل رقم هاتف مكون من 11 رقم")]
    string PhoneNumber,
    [Required(ErrorMessage = "من فضلك ادخل المحافظه التابع لها")]
    string Governorate, 
    string City, 
    string? Village,
    [Required(ErrorMessage = "يجب اختيار وحده صحيه يتبع لها المستخدم لاكمال التحديث")]
    Guid HealthCareCenterId);
