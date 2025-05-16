using Ebret4m4n.Shared.CutomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record UpdateCityAdminDto(
    [Required(ErrorMessage = "يجب ادخال الاسم الاول")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    string FirstName,
    [Required(ErrorMessage = "يجب ادخال اسم العائله")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    string LastName,
    [Required(ErrorMessage = "من فضلك ادخل عنوان بريد الكتروني")]
    [EmailAddress(ErrorMessage = "من فضلك ادخل بريد الكنروني صحيح")]
    string Email,
    [Required(ErrorMessage = "من فضلك ادخل المركز الذي سيتم ادارته من قبل هذ المستخدم")]
    string City);
