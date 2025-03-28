using Ebret4m4n.Shared.DTOs.MedicalStaffDtos;
using Ebret4m4n.Shared.DTOs.InventoriesDtos;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;



namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "cityAdmin")]
[ApiController]
public class CityAdminController
    (IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager,
    IHubContext<NotificationHub> hubContext) : ControllerBase
{

    [HttpGet("healthcareCenter-village")]
    public async Task<IActionResult> GetHealthCareInVillageAsync()
    {
        var city = User.FindFirst("city")!.Value;

        var healthCareCenters = await unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.City == city, false)
            .Select(hc => new HealthCaresListDto(hc.HealthCareCenterId,hc.HealthCareCenterName))
            .ToListAsync();

        if (healthCareCenters is null)
            throw new BadRequestException("لم يتم اضافه اي وحدات صحيه بعد لهذا المركز");

        var response = new GeneralResponse<List<HealthCaresListDto>>(StatusCodes.Status200OK, healthCareCenters);

        return Ok(response);
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    public async Task<IActionResult> HealthCareDetails(Guid id)
    {
        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
            .FindAsync(hc => hc.HealthCareCenterId == id, false, ["Inventories"]);

        if (healthCareCenter is null)
            throw new NotFoundBadRequest("لا توجد وحده صحيه بهذا الرقم");

        var healthCareStaff =
            unitOfWork.StaffRepo.FindByCondition(staff => staff.HCCenterId == healthCareCenter.HealthCareCenterId, false, ["User"])
            .ToList();

        var organizerName =
            healthCareStaff.Where(hc => hc.StaffRole == StaffRole.Organizer)
            .Select(org => $"{org.User.FirstName} {org.User.LastName}")
            .FirstOrDefault();

        var doctorName = healthCareStaff.Where(hc => hc.StaffRole == StaffRole.Doctor)
            .Select(doc => $"{doc.User.FirstName} {doc.User.LastName}")
            .FirstOrDefault();

        var healthCareDetails = healthCareCenter.Adapt<HealthCareDetailsDto>();

        healthCareDetails.OrganizerName = organizerName;
        healthCareDetails.DoctorName = doctorName;
        healthCareDetails.Inventories = healthCareCenter.Inventories.Adapt<List<InventoryDto>>();

        var response = new GeneralResponse<HealthCareDetailsDto>(StatusCodes.Status200OK, healthCareDetails); ;

        return Ok(response);
    }

    [HttpPost("medical-postion-add")]
    public async Task<IActionResult> AddMedicalPostion(MedicalStaffDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var healthCare =
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == model.HealthCareCenterId, false);

        if (healthCare is null)
            throw new BadRequestException("لم يتم العثور علي هذه الوحده الصحيه");

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new BadRequestException("لم يتم انشاء هذا المستخدم");

        await userManager.AddToRoleAsync(user, model.StaffRole);

        var medicalStaff = (model, healthCare).Adapt<MedicalStaff>();

        medicalStaff.UserId = user.Id;
        medicalStaff.CityAdminStaffId = adminId;
            
        await unitOfWork.StaffRepo.AddAsync(medicalStaff);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            throw new BadRequestException("لم يتم حفظ الباينات اللرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه هذا المستخدم بنجاح");

        return Ok(response);
    }

    [HttpPost("healthCare-add")]
    public async Task<IActionResult> AddHealthCareCenter(AddHealthCareDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(new GeneralResponse<string>(StatusCodes.Status422UnprocessableEntity, "الرجاء التاكد ان جميع المدخلات صحيحه"));

        var healthCare = model.Adapt<HealthCareCenter>();

        await unitOfWork.HealthCareCenterRepo.AddAsync(healthCare);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ الوحده الصحيه");

        return CreatedAtAction("HealthCareDetails", new { Id = healthCare.HealthCareCenterId });
    }


    [HttpGet("complaints")]
    public async Task<IActionResult> Complaints()
    {
        var city = User.FindFirst("city")!.Value;

        var complaints = await unitOfWork.ComplaintRepo
            .FindByCondition(c => c.User.City == city && c.IsResolved == false, false, "User")
            .Select(C => C.Adapt<ComplaintsDto>())
            .ToListAsync();
            

        if (complaints is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله");

        var response = new GeneralResponse<List<ComplaintsDto>> (StatusCodes.Status200OK, complaints);

        return Ok(response);
    }

    [HttpGet("{id:guid}/complaint")]
    public async Task<IActionResult> Complaint(Guid id)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(c => c.Id == id, false, "User");

        if (complaint is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله لهذا المستخدم");

        var healthCare = 
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == complaint.User.HealthCareCenterId, false);

        var complaintDto = (complaint, healthCare).Adapt<ComplaintDto>();

        var response = new GeneralResponse<ComplaintDto>(StatusCodes.Status200OK, complaintDto);

        return Ok(response);
    }

    [HttpPut("{complaintId:guid}/handle-complaint")]
    public async Task<IActionResult> HandleComplaint(Guid complaintId)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(C => C.Id == complaintId, false, "User");

        if (complaint is null)
            throw new NotFoundException("هذه الشكوي غير موجوده او تم حذفها");

        var notification = Utility.CreateNotification("الشكاوي", "تم حل الشكوي الخاص بك الرجاء التوجه للوحد الصحيه لاستكمال", complaint.UserId);

        complaint.IsResolved = true;

        unitOfWork.ComplaintRepo.Update(complaint);

        await SaveNotification(notification);

        var result = await unitOfWork.SaveAsync();

        if (result == 0) 
            throw new BadRequestException("لم يتم حفظ رساله التنبيه الرجاء المحاوله مره اخري");

        var notificationDto = notification.Adapt<NotificationDto>();

        await hubContext.Clients.User(complaint.UserId).SendAsync("NotificationMessage", notificationDto);
        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم ارسال التنيه بنجاح");

        return Ok(response);
    }

    [HttpGet("organizers")]
    public IActionResult Organizers()
    {
        var organizers = GetMedicalStaff(StaffRole.Organizer);

        var organizersDto = organizers.Adapt<List<MedicalStaffDetailsDto>>();

        var response = new GeneralResponse<List<MedicalStaffDetailsDto>>(StatusCodes.Status200OK, organizersDto);

        return Ok(organizersDto);
    }

    [HttpGet("doctors")]
    public IActionResult Doctors()
    {
        var doctors = GetMedicalStaff(StaffRole.Doctor);

        var doctorsDto = doctors.Adapt<List<MedicalStaffDetailsDto>>();

        var response = new GeneralResponse<List<MedicalStaffDetailsDto>>(StatusCodes.Status200OK, doctorsDto);

        return Ok(doctorsDto);
    }

    private List<MedicalStaff> GetMedicalStaff(StaffRole role)
    {
        var adminId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var staff =
            unitOfWork.StaffRepo.FindByCondition(organizer => organizer.CityAdminStaffId == adminId
            && organizer.StaffRole == role, false, ["User"])
            .ToList() ?? [];

        return staff;
    }

    private async Task SaveNotification(Notification notification)
        => await unitOfWork.NotificationRepo.AddAsync(notification);
}
