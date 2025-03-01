using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Ebret4m4n.Shared.DTOs.JobApplicationsDtos;
using Ebret4m4n.Shared.DTOs.MedicalApplicationsDtos;
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

        //TypeAdapterConfig<ChildVaccinDto,Vaccine>.NewConfig()
        //    .Map(dest=>dest.na)



        TypeAdapterConfig<BaseVaccine, Vaccine>.NewConfig()
            .Map(dest => dest.Name, src => src.name)
            .Map(dest => dest.DocesRequired, src => src.docesRequired)
            .Map(dest => dest.ChildAge, src => src.childAge);

        TypeAdapterConfig<(BaseVaccine baseVaccine, string childId), Vaccine>.NewConfig()
            .Map(dest => dest.Id, src => src.baseVaccine.id)
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
            .Map(dest => dest.UserId, src => src.parentId)
            .Map(dest => dest, src => src.appointmentDto);

        TypeAdapterConfig<(Appointment appointment, string childName), AppointmentDto>.NewConfig()
            .Map(dest => dest.ChildName, src => src.childName)
            .Map(dest => dest.Day, src => src.appointment.Date.ToString("dddd"))
            .Map(dest => dest.Date, src => src.appointment.Date);

        TypeAdapterConfig<Appointment, UserReservationDto>.NewConfig()
            .Map(dest => dest.ChildName, src => src.Child.Name)
            .Map(dest => dest.VaccineName, src => src.Vaccine.Name)
            .Map(dest => dest.RestOfDaysToAppointment,
            src => Math.Floor((src.Date - DateTime.Today).TotalDays))
            .Map(dest => dest, src => src);

        TypeAdapterConfig<(MedicalApplicationDto applicationDto, string userId), MedicalApplication>.NewConfig()
            .Map(dest => dest.UserId, src => src.userId)
            .Map(dest => dest, src => src.applicationDto);

        TypeAdapterConfig<MedicalApplication, MedicalPositionRequestsDto>.NewConfig()
            .Map(dest => dest.ApplicantName, src => src.User.UserName)
            .Map(dest => dest.ApplicantLocation, src => $"{src.User.Governorate},{src.User.City},{src.User.Village}");


        TypeAdapterConfig<(ApplicationUser user, HealthCareCenter hcCenter,string MedicalNumber), MedicalStaff>.NewConfig()
            .Map(dest => dest.UserId, src => src.user.Id)
            .Map(dest => dest.MedicalNumber, src => src.MedicalNumber)
            .Map(dest => dest.HealthCareCenterGovernorate, src => src.hcCenter.Governorate)
            .Map(dest => dest.HealthCareCenterCity, src => src.hcCenter.City)
            .Map(dest => dest.HealthCareCenterVillage, src => src.hcCenter.Village)
            .Map(dest => dest.HCCenterId, src => src.hcCenter.HealthCareCenterId)
            .Map(dest => dest, src => src.hcCenter);

    }
}
