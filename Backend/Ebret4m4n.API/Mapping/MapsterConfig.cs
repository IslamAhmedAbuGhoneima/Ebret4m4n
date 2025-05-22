using Ebret4m4n.Shared.DTOs.AdminsDto.MinistryOfHealthAdminDtos;
using Ebret4m4n.Shared.DTOs.AdminsDto.GovernorateAdminDtos;
using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.MedicalStaffDtos;
using Ebret4m4n.Shared.DTOs.InventoriesDtos;
using Ebret4m4n.Shared.DTOs.AppointmentDtos;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Shared.DTOs.OrderDtos;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Ebret4m4n.Shared.DTOs.ChatDtos;
using Ebret4m4n.Entities.Models;
using Stripe.Checkout;
using Mapster;


namespace Ebret4m4n.API.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<RegisterDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);

        TypeAdapterConfig<AddCityAdminDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);


        TypeAdapterConfig<AddMedicalStaffDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);

        TypeAdapterConfig<AddGovernorateAdminDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);

        TypeAdapterConfig<UpdateCityAdminDto,ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);

        TypeAdapterConfig<UpdateGovernorateAdminDto, ApplicationUser>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email);

        TypeAdapterConfig<(AddChildDto childDtod, string parentId), Child>
            .NewConfig()
            .Map(dest => dest, src => src.childDtod)
            .Map(dest => dest.UserId, src => src.parentId);

        TypeAdapterConfig<Child, ChildDto>.NewConfig()
        .Map(dest => dest.FilePath, src => src.HealthReportFiles.Select(f => f.FilePath).ToList());

        TypeAdapterConfig<Child, ChildDataDto>.NewConfig()
        .Map(dest => dest.FilePath, src => src.HealthReportFiles.Select(f => f.FilePath).ToList());

        TypeAdapterConfig<BaseVaccine, Vaccine>.NewConfig()
            .Map(dest => dest.Name, src => src.name)
            .Map(dest => dest.ChildAge, src => src.childAge);

        TypeAdapterConfig<(BaseVaccine baseVaccine, string childId), Vaccine>.NewConfig()
            .Map(dest => dest.Id, src => src.baseVaccine.id)
            .Map(dest => dest.Name, src => src.baseVaccine.name)
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
            .Map(dest => dest.RestOfDaysToAppointment,
            src => Math.Floor((src.Date - DateTime.Today).TotalDays))
            .Map(dest => dest, src => src);

        TypeAdapterConfig<(ApplicationUser user, HealthCareCenter hcCenter, string MedicalNumber), MedicalStaff>.NewConfig()
            .Map(dest => dest.UserId, src => src.user.Id)
            .Map(dest => dest.HealthCareCenterGovernorate, src => src.hcCenter.Governorate)
            .Map(dest => dest.HealthCareCenterCity, src => src.hcCenter.City)
            .Map(dest => dest.HealthCareCenterVillage, src => src.hcCenter.Village)
            .Map(dest => dest.HCCenterId, src => src.hcCenter.HealthCareCenterId)
            .Map(dest => dest, src => src.hcCenter);

        TypeAdapterConfig<(AddMedicalStaffDto staffDto, HealthCareCenter hcCenter), MedicalStaff>.NewConfig()
            .Map(dest => dest.HealthCareCenterGovernorate, src => src.hcCenter.Governorate)
            .Map(dest => dest.HealthCareCenterCity, src => src.hcCenter.City)
            .Map(dest => dest.HealthCareCenterVillage, src => src.hcCenter.Village)
            .Map(dest => dest.HCCenterId, src => src.hcCenter.HealthCareCenterId)
            .Map(dest => dest, src => src.staffDto)
            .Map(dest => dest, src => src.hcCenter);

        TypeAdapterConfig<(UpdateMedicalStaffDto staffDto, HealthCareCenter hcCenter), MedicalStaff>.NewConfig()
            .Map(dest => dest.HealthCareCenterGovernorate, src => src.hcCenter.Governorate)
            .Map(dest => dest.HealthCareCenterCity, src => src.hcCenter.City)
            .Map(dest => dest.HealthCareCenterVillage, src => src.hcCenter.Village)
            .Map(dest => dest.HCCenterId, src => src.hcCenter.HealthCareCenterId)
            .Map(dest => dest, src => src.staffDto)
            .Map(dest => dest, src => src.hcCenter);

        TypeAdapterConfig<(Complaint complaint, HealthCareCenter hcCenter), ComplaintDto>.NewConfig()
            .Map(dest => dest.UserName, src => $"{src.complaint.User.FirstName} {src.complaint.User.LastName}")
            .Map(dest => dest.HCLocation, src => $"{src.hcCenter.Governorate},{src.hcCenter.City},{src.hcCenter.Village}")
            .Map(dest => dest, src => src.hcCenter)
            .Map(dest => dest, src => src.complaint)
            .Map(dest => dest, src => src.complaint.User);


        TypeAdapterConfig<GovernorateAdminStaff, GovernorateAdminsDto>.NewConfig()
            .Map(dest => dest, src => src.User);

        TypeAdapterConfig<CityAdminStaff, CityAdminsDto>.NewConfig()
            .Map(dest => dest, src => src.User);

        TypeAdapterConfig<Order, CityOrderDetails>.NewConfig()
            .Map(dest => dest.City, src => src.CityAdminStaff.City)
            .Map(dest => dest, src => src);

        TypeAdapterConfig<Order, HealthCareOrdersDto>.NewConfig()
            .Map(dest => dest.HealthCareCenterName, src => src.MedicalStaff.HealthCareCenterName)
            .Map(dest => dest, src => src);

        TypeAdapterConfig<Order, GovernorateOrderDto>.NewConfig()
            .Map(dest => dest.Governorate, src => src.GovernorateAdminStaff.Governorate)
            .Map(dest => dest, src => src);

        TypeAdapterConfig<Complaint, ComplaintsDto>.NewConfig()
            .Map(dest => dest.HealthCareCenterName, src => src.User.HealthCareCenter!.HealthCareCenterName)
            .Map(dest => dest.UserName, src => $"{src.User.FirstName} {src.User.LastName}");

        TypeAdapterConfig<CityAdminStaff, CityRecordDetailsDto>.NewConfig()
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.HealthCares, src => src.HealthCareCenters.Adapt<List<HealthCaresListDto>>())
            .Map(dest => dest.HealthCareCount, src => src.HealthCareCenters.Count);


        TypeAdapterConfig<GovernorateAdminStaff, GovernorateDetailsDto>.NewConfig()
            .Map(dest => dest.Cities, src => src.CityAdminStaffs.Select(city => city.Adapt<CitiesDto>()).ToList())
            .Map(dest => dest.CityCounts, src => src.CityAdminStaffs.Count)
            .Map(dest => dest.HealthCareCount, src => src.CityAdminStaffs.Sum(city => city.HealthCareCenters.Count))
            .Map(dest => dest, src => src.User);

        TypeAdapterConfig<HealthCareCenter, HealthCareDetailsDto>.NewConfig()
            .MapToConstructor(true);

        TypeAdapterConfig<Chat, ChatUsersListDto>.NewConfig()
            .Map(dest => dest, src => src.Sender);

        TypeAdapterConfig<Appointment, ComingChildrenDto>.NewConfig()
            .Map(dest => dest.ParentName, src => src.User.FirstName)
            .Map(dest => dest.ChildName, src => src.Child.Name);

        TypeAdapterConfig<(Session session, string parentId, string childId), Transaction>.NewConfig()
            .Map(dest => dest.Amount, src => src.session.AmountTotal)
            .Map(dest => dest.ChildId, src => src.childId)
            .Map(dest => dest.ParentId, src => src.parentId)
            .Map(dest => dest.SessionId, src => src.session.Id);

        TypeAdapterConfig<CityAdminStaff, HealthCaresCityDto>.NewConfig()
            .Map(dest => dest.HealthCaresList, src => src.HealthCareCenters.Adapt<List<HealthCaresListDto>>())
            .Map(dest => dest.CityRecordDetails.HealthCareCount, src => src.HealthCareCenters.Count)
            .Map(dest => dest.CityRecordDetails, src => src.User.Adapt<CityRecordDetailsDto>())
            .Map(dest => dest.CityRecordDetails.MainInventories, src => src.MainInventories.Adapt<List<InventoryDto>>());

        TypeAdapterConfig<CityAdminStaff, CityAdminDetailsDto>.NewConfig()
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.Governorate, src => src.Governorate)
            .Map(dest => dest, src => src.User);

        TypeAdapterConfig<MedicalStaff, MedicalStaffDetailsDto>.NewConfig()
            .Map(dest => dest.HCCenterId, src => src.HCCenterId)
            .Map(dest => dest, src => src.User);
    }
}
