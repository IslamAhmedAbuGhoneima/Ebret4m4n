using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using System.Text.Json;
using Stripe.Checkout;
using Mapster;


namespace Ebret4m4n.API.Utilites;

public class Utility
{
    public static List<string> InventoryAntigens()
    {
        var antigenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "Antigens.json");

        if (!Path.Exists(antigenFilePath))
            throw new NotFoundException("لم يتم العثور علي السمتضاتات الخاصه بالقاحات");

        using var stream = new FileStream(antigenFilePath, FileMode.Open);

        var antigens = JsonSerializer.Deserialize<List<string>>(stream);

        if (antigens == null)
            throw new BadRequestException("لم ايجاد اي مستضادات لقاح");

        return antigens;
    }

    public static List<BaseVaccine> ReadBaseVaccineFromJson()
    {
        string path =
                Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "vaccines.json");

        if (!Path.Exists(path))
            throw new FileNotFoundException("لم يتم استرجاع القاحات الرجاء التواصل مع الدعم الفني");

        using var strem = new FileStream(path, FileMode.Open);
        var baseVaccines = JsonSerializer.Deserialize<List<BaseVaccine>>(strem);

        if (baseVaccines == null)
            throw new BadRequestException("حدث خطا ما اثناء تسجيل الطفل الرجاء الاتصال بالدعم الفني للمساعده");

        return baseVaccines;
    }

    public static List<Vaccine> ChildBaseVaccines()
    {
        var baseVaccines = ReadBaseVaccineFromJson();

        var vaccines = baseVaccines.Adapt<List<Vaccine>>();

        return vaccines;
    }

    public static Notification CreateNotification(string title, string message, string userId)
    {
        var notification = new Notification
        {
            Title = title,
            Message = message,
            UserId = userId
        };

        return notification;
    }

    public static List<Vaccine> ChildVaccines(List<string>? childVaccines, string childId)
    {
        var vaccines = ChildBaseVaccines();

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        if (childVaccines is not null)
        {
            foreach (var vaccineName in childVaccines)
            {
                var vaccine = vaccines.FirstOrDefault(v => v.Name == vaccineName);

                if (vaccine is null)
                    throw new BadRequestException("لايوجد لقاح بهذا الاسم تاكد من ان اسماء اللقاحات المختاره صحيحه");

                vaccine.IsTaken = true;
            }
        }
        return vaccines;
    }

    public static List<HealthReportFile>? SaveReportFiles(List<IFormFile> imageFiles, string childId)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "ChildReports");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        List<HealthReportFile> healthReports = [];

        foreach (var file in imageFiles)
        {
            if (file.Length <= 5_242_880)
            {
                var fileName = Guid.NewGuid().ToString();
                var fileExtenstion = Path.GetExtension(file.FileName);

                var reportPath = Path.Combine(path, $"{fileName}{fileExtenstion}");

                using (var stream = new FileStream(reportPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                healthReports.Add(new HealthReportFile
                {
                    FilePath = $"/Files/ChildReports/{fileName}{fileExtenstion}",
                    ChildId = childId
                });
            }
            else
                throw new FileLoadException("لا يمكن تحميل الملف بهذا الحجم");
        }
        return healthReports;
    }

    public static string UploadChatFile(IFormFile file)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "ChatFiles");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (file.Length > 5_242_880)
            throw new BadRequestException("لايمكن رفع ملف بهذا الحجم");

        var fileName = Guid.NewGuid().ToString();
        var fileExtension = Path.GetExtension(file.FileName);

        var filePath = Path.Combine(path, $"{fileName}{fileExtension}");

        using (var strem = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(strem);
        }

        return $"/Files/ChatFiles/{fileName}{fileExtension}";
    }

    public static async Task<Session> CreateSessionPayment(string parentId, string parentEmail, string childId, string childName)
    {
        var options = new SessionCreateOptions
        {
            Currency = "usd",
            Mode = "payment",
            ClientReferenceId = parentId,
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"تسجيل واضافه جدول التطعيمات للطفل {childName}",
                        },
                        UnitAmount = 5000,
                    },
                    Quantity = 1,
                },
            },
            Metadata = new Dictionary<string, string>
            {
                { "childId", childId },
                { "childName", childName },
                { "parentEmail", parentEmail}
            },
            SuccessUrl = $"http://localhost:4200/payment/success/{childId}",
            CancelUrl = "http://localhost:4200/payment/cancel"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;
    }

    public static DateTime GetDateOfNextDay(string dayName)
    {
        if(!Enum.TryParse(dayName, out DayOfWeek targetDay))
        {
            throw new BadRequestException("اسم يوم غير صحيح");
        }

        var today = DateTime.Today;

        // Calculate days to add to get to the target day
        int daysUntilTargetDay = ((int)targetDay - (int)today.DayOfWeek + 7) % 7;

        // If today is the target day, we want next week's occurrence
        if (daysUntilTargetDay == 0) 
            daysUntilTargetDay = 7;

        // Calculate the next occurrence of the target day
        DateTime nextTargetDay = today.AddDays(daysUntilTargetDay);

        return nextTargetDay.Date;
    }
}
