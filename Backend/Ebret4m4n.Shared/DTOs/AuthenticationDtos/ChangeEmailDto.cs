using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.DTOs.AuthenticationDtos;

public record ChangeEmailDto(string UserId, 
    [EmailAddress(ErrorMessage ="هذا الايميل غير صالح الرجاء ادخال ايميل صحيح")] string NewEmail, 
    string Token);
