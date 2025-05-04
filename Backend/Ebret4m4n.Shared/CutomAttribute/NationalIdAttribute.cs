using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ebret4m4n.Shared.CutomAttribute;

public class NationalIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? id = value as string;

        if (string.IsNullOrEmpty(id))
            return new ValidationResult("يجب ادخال رقم قومي");

        if (!Regex.IsMatch(id, @"^\d{14}$"))
            return new ValidationResult("يجب ان يكون الرقم القومي مكون من 14 رقم");

        if (id[0] != '2' && id[0] != '3')
            return new ValidationResult("رقم قومي غير صالح يجب ان يبدا الرقم القومي ب 3 او 2");


        string birthDatePart = id.Substring(1, 6);
        string fullYearPrefix = id[0] == '2' ? "19" : "20";
        string fullDate = $"{fullYearPrefix}{birthDatePart}";

        if (!DateTime.TryParseExact(fullDate, "yyyyMMdd", null, DateTimeStyles.None, out _))
            return new ValidationResult("تاريخ ملاد غير صحيح");

        return ValidationResult.Success;
    }
}
