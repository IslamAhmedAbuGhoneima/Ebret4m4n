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
}
