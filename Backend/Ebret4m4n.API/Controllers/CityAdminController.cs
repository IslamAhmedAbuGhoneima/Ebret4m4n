using Ebret4m4n.Shared.DTOs.MedicalStaffDtos;
using Ebret4m4n.Shared.DTOs.InventoriesDtos;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.SignalRDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Ebret4m4n.API.Hubs;
using Mapster;



namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityAdminController
    (IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager,
    IHubContext<NotificationHub> hubContext,
    IEmailSender emailSender) : ControllerBase
{

    [HttpGet("healthcareCenter-village")]
    [Authorize(Roles = "cityAdmin")]
    public IActionResult GetHealthCareInVillage()
    {
        string adminCity = User.FindFirst("city")!.Value;

        var healthCareCenters = unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.City == adminCity, false)
            .Select(hc => hc.Adapt<HealthCaresListDto>())
            .ToList();

        if (healthCareCenters is null)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم اضافه اي وحدات صحيه بعد لهذا المركز"));

        var response = GeneralResponse<List<HealthCaresListDto>>.SuccessResponse(healthCareCenters);

        return Ok(response);
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    [Authorize(Roles = "cityAdmin,governorateAdmin,admin")]
    public async Task<IActionResult> HealthCareDetails(Guid id)
    {
        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
            .FindAsync(hc => hc.HealthCareCenterId == id, false, ["Inventories"]);

        if (healthCareCenter is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا توجد وحده صحيه بهذا الرقم"));

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

        var response = GeneralResponse<HealthCareDetailsDto>.SuccessResponse(healthCareDetails); ;

        return Ok(response);
    }

    [HttpPost("medical-postion-add")]
    [Authorize(Roles = "cityAdmin")]
    public async Task<IActionResult> AddMedicalPostion(AddMedicalStaffDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<object>.FailureResponse(ModelState));

        var adminId = User.FindFirst("id")!.Value;

        var healthCare =
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == model.HealthCareCenterId, false);

        if (healthCare is null)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم العثور علي هذه الوحده الصحيه"));

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم انشاء هذا المستخدم حاول مره اخري"));

        await userManager.AddToRoleAsync(user, model.StaffRole);

        var medicalStaff = (model, healthCare).Adapt<MedicalStaff>();

        medicalStaff.UserId = user.Id;
        medicalStaff.CityAdminStaffId = adminId;
            
        await unitOfWork.StaffRepo.AddAsync(medicalStaff);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ الباينات اللرجاء المحاوله مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم اضافه هذا المستخدم بنجاح");

        return Ok(response);
    }

    [HttpPost("healthCare-add")]
    [Authorize(Roles = "cityAdmin")]
    public async Task<IActionResult> AddHealthCareCenter(AddHealthCareDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<string>.FailureResponse("الرجاء التاكد ان جميع المدخلات صحيحه"));

        var adminId = User.FindFirst("id")!.Value;

        var healthCare = model.Adapt<HealthCareCenter>();
        healthCare.CityAdminId = adminId;

        await unitOfWork.HealthCareCenterRepo.AddAsync(healthCare);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ الوحده الصحيه"));

        var response = GeneralResponse<string>.SuccessResponse("تم انشاء الوحده الصحيه بنجاح");

        return Ok(response);
    }

    [HttpGet("complaints")]
    [Authorize(Roles = "cityAdmin")]
    public IActionResult Complaints()
    {
        var city = User.FindFirst("city")!.Value;

        var complaints = unitOfWork.ComplaintRepo
            .FindByCondition(c => c.User.City == city && c.IsResolved == false, false, "User")
            .Select(C => C.Adapt<ComplaintsDto>())
            .ToList();
            

        if (complaints is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا توجد شكاوي مسجله"));

        var response = GeneralResponse<List<ComplaintsDto>>.SuccessResponse(complaints);

        return Ok(response);
    }

    [HttpGet("{id:guid}/complaint")]
    [Authorize(Roles = "cityAdmin")]
    public async Task<IActionResult> Complaint(Guid id)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(c => c.Id == id, false, "User");

        if (complaint is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا توجد شكاوي مسجله لهذا المستخدم"));

        var healthCare = 
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == complaint.User.HealthCareCenterId, false);

        var complaintDto = (complaint, healthCare).Adapt<ComplaintDto>();

        var response = GeneralResponse<ComplaintDto>.SuccessResponse(complaintDto);

        return Ok(response);
    }

    [HttpPut("{complaintId:guid}/handle-complaint")]
    [Authorize(Roles = "cityAdmin")]
    public async Task<IActionResult> HandleComplaint(Guid complaintId)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(C => C.Id == complaintId, false, "User");

        if (complaint is null)
            return NotFound(GeneralResponse<string>.FailureResponse("هذه الشكوي غير موجوده او تم حذفها"));

        var notification = Utility.CreateNotification("الشكاوي", "تم حل الشكوي الخاص بك الرجاء التوجه للوحد الصحيه لاستكمال", complaint.UserId);

        complaint.IsResolved = true;

        unitOfWork.ComplaintRepo.Update(complaint);

        await SaveNotification(notification);

        var result = await unitOfWork.SaveAsync();

        if (result == 0) 
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم حفظ رساله التنبيه الرجاء المحاوله مره اخري"));

        var notificationDto = notification.Adapt<NotificationDto>();

        await hubContext.Clients.User(complaint.UserId).SendAsync("NotificationMessage", notificationDto);

        await emailSender.SendEmailAsync(complaint.User.Email!, "تنبيه", "<p>تم حل الشكوي الخاص بك الرجاء التوجه للوحد الصحيه لاستكمال التطعيمات لاطفالك</p>");

        var response = GeneralResponse<string>.SuccessResponse("تم ارسال التنيه بنجاح");

        return Ok(response);
    }

    [HttpGet("organizers")]
    [Authorize(Roles = "cityAdmin")]
    public IActionResult Organizers()
    {
        var organizers = GetMedicalStaff(StaffRole.Organizer);

        var organizersDto = organizers.Adapt<List<MedicalStaffDetailsDto>>();

        var response = GeneralResponse<List<MedicalStaffDetailsDto>>.SuccessResponse(organizersDto);

        return Ok(organizersDto);
    }

    [HttpGet("doctors")]
    [Authorize(Roles = "cityAdmin")]
    public IActionResult Doctors()
    {
        var doctors = GetMedicalStaff(StaffRole.Doctor);

        var doctorsDto = doctors.Adapt<List<MedicalStaffDetailsDto>>();

        var response = GeneralResponse<List<MedicalStaffDetailsDto>>.SuccessResponse(doctorsDto);

        return Ok(doctorsDto);
    }

    private List<MedicalStaff> GetMedicalStaff(StaffRole role)
    {
        var adminId = User.FindFirst("id")!.Value;

        var staff =
            unitOfWork.StaffRepo.FindByCondition(organizer => organizer.CityAdminStaffId == adminId
            && organizer.StaffRole == role, false, ["User"])
            .ToList() ?? [];

        return staff;
    }

    private async Task SaveNotification(Notification notification)
        => await unitOfWork.NotificationRepo.AddAsync(notification);
}
