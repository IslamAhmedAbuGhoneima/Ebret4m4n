using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Ebret4m4n.Shared.CutomAttribute;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;

        if (string.IsNullOrEmpty(email))
            return new ValidationResult("لا يمكن ان يكون البريد الالكتروني فارغ");


        var userManger = validationContext.GetRequiredService<UserManager<ApplicationUser>>();

        ApplicationUser? user = userManger!.FindByEmailAsync(email)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        return user is null
            ? ValidationResult.Success
            : new ValidationResult("البريد الإلكتروني مسجل مسبقاً");
    }
}
