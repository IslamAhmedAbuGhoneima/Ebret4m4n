using Ebret4m4n.Shared.CutomAttribute;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;

public record AddCityAdminDto
{
    [Required(ErrorMessage = "يجب ادخال الاسم الاول")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    public string FirstName { get; init; } = null!;

    [Required(ErrorMessage = "يجب ادخال اسم العائله")]
    [Length(3, 20, ErrorMessage = "يجب ان يكون طول الاسم من 3 الي 20 حرف")]
    public string LastName { get; init; } = null!;

    [Required(ErrorMessage = "من فضلك ادخل عنوان بريد الكتروني")]
    [EmailAddress(ErrorMessage = "من فضلك ادخل بريد الكنروني صحيح")]
    [UniqueEmail]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "من فضلك ادخل مرور")]
    [MinLength(8, ErrorMessage = "لا يجب ان تقل كلمه المرور عن 8 احرف")]
    public string Password { get; init; } = null!;

    [Required(ErrorMessage = "من فضلك ادخل المحافظه التابع لها")]
    public string Governorate { get; init; } = null!;

    [Required(ErrorMessage = "من فضلك ادخل المركز الذي سيتم ادارته من قبل هذ المستخدم")]
    public string City { get; init; } = null!;

    public string Role { get; init; } = "cityAdmin";
}
