using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Mapster;

namespace Ebret4m4n.API.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<RegisterDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => $"{src.FirstName} {src.LastName}");

        TypeAdapterConfig<(AddChildDto childDtod, string parentId), Child>
            .NewConfig()
            .Map(dest => dest, src => src.childDtod)
            .Map(dest => dest.UserId, src => src.parentId);

        TypeAdapterConfig<(BaseVaccine baseVaccine, string childId), Vaccine>.NewConfig()
            .Map(dest => dest.Name, src => src.baseVaccine.name)
            .Map(dest => dest.DocesRequired, src => src.baseVaccine.docesRequired)
            .Map(dest => dest.DocesTaken, src => src.baseVaccine.docesRequired)
            .Map(dest => dest.ChildAge, src => src.baseVaccine.childAge)
            .Map(dest => dest.IsTaken, src => true)
            .Map(dest => dest.ChildId, src => src.childId);

        TypeAdapterConfig<UpdateChildDto, Child>.NewConfig()
            .IgnoreNullValues(true);

        TypeAdapterConfig<(AddAppointmentDto appointmentDto, HealthCareCenter HC, string parentId), Appointment>.NewConfig()
            .Map(dest => dest.HealthCareCenterId, src => src.HC.HealthCareCenterId)
            .Map(dest => dest.Location, src => $"{src.HC.Governorate},{src.HC.City},{src.HC.Village}")
            .Map(dest => dest.Status, src => BookStatus.Booked)
            .Map(dest => dest.UserId, src => src.parentId)
            .Map(dest => dest, src => src.appointmentDto);

        TypeAdapterConfig<(Appointment appointment, string childName), AppointmentDto>.NewConfig()
            .Map(dest => dest.ChildName, src => src.childName)
            .Map(dest => dest.Day, src => src.appointment.Date.ToString("dddd"))
            .Map(dest => dest.Date, src => src.appointment.Date);

            
    }
}
