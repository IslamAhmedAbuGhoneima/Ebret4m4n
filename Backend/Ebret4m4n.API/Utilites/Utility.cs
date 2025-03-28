using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using System.Text.Json;
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

    public static List<Vaccine> ChildBaseVaccines()
    {
        string path =
                Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "vaccines.json");

        if (!Path.Exists(path))
            throw new FileNotFoundException("لم يتم استرجاع القاحات الرجاء التواصل مع الدعم الفني");

        using var strem = new FileStream(path, FileMode.Open);
        var baseVaccines = JsonSerializer.Deserialize<List<BaseVaccine>>(strem);

        if (baseVaccines == null)
            throw new BadRequestException("حدث خطا ما اثناء تسجيل الطفل الرجاء الاتصال بالدعم الفني للمساعده");

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

    public static List<Vaccine> ReadVaccinesFromJsonFile(List<string>? childVaccines, string childId)
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
}
