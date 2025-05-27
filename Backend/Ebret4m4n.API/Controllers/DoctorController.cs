using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles ="doctor")]
[ApiController]
public class DoctorController
    (IUnitOfWork unitOfWork,IEmailSender emailSender): ControllerBase
{
    [HttpGet("children-disease")]
    public IActionResult GetChildrenWithDisease()
    {
        var doctorHcCenterId = User.FindFirst("healthCareId")!.Value;

        var children = unitOfWork.ChildRepo.FindByCondition(
            c => (c.PatientHistory != null || (c.HealthReportFiles != null && c.HealthReportFiles.Any())) &&
            (c.IsNormal != false && (c.Vaccines == null || c.Vaccines.Count == 0) ) &&
            c.User.HealthCareCenterId.ToString() == doctorHcCenterId,
        false, ["User", "Vaccines", "HealthReportFiles"]).ToList();


        var childrenDto = children.Adapt<List<ChildDto>>();
        var response = GeneralResponse<List<ChildDto>>.SuccessResponse(childrenDto);

        return Ok(response);
    }

    [HttpGet("{childId}/child-data")]
    public async Task<IActionResult> GetChildData(string childId)
    {
        var child = 
            await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false, ["HealthReportFiles"]);

        var childDto = child.Adapt<ChildDto>();

        var response = GeneralResponse<ChildDto>.SuccessResponse(childDto);

        return Ok(response);
    }

    [HttpPost("{childId}/suspend")]
    public async Task<IActionResult> SuspendChildVaccine(string childId)
    {
        var child = 
            await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false, ["User"]);

        if (child.IsNormal == false)
            return BadRequest(GeneralResponse<string>.FailureResponse("تم اضافه الطفل الي قائمه المؤجلين من قبل"));

        child.IsNormal = false;

        unitOfWork.ChildRepo.Update(child);
        int result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تاجيل اللقاحات لهذا الطفل حاول مره اخري"));

        await emailSender.SendEmailAsync(child.User.Email!, "تاجيل التطعيمات", $"<p>بناء علي التحاليل المقدمه تم تاجيل التطعيم لطفلك : {child.Name}</p>");

        var response = GeneralResponse<string>.SuccessResponse("تم تاجيل اللقاحات لهذا الطفل");

        return Ok(response);
    }

    [HttpGet("children-suspended")]
    public IActionResult SuspendedChildren()
    {
        var doctorHcCenterId = User.FindFirst("healthCareId")!.Value;

        var children = unitOfWork.ChildRepo.FindByCondition(
            child => child.IsNormal == false && child.User.HealthCareCenterId.ToString() == doctorHcCenterId, false, ["User"])
            .Select(child => child.Adapt<SuspendedChildrenDto>()).ToList() ?? [];

        var response = GeneralResponse<List<SuspendedChildrenDto>>.SuccessResponse(children);

        return Ok(response);
    }

    [HttpPost("{childId}/add-normal-vaccine")]
    public async Task<IActionResult> AddNormalVacinnes(string childId)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(c => c.Id == childId, true, ["Vaccines"]);

        if (child == null)
            return NotFound(GeneralResponse<string>.FailureResponse("الطفل غير موجود"));

        if(child.Vaccines != null && child.Vaccines.Count > 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("هذا الطفل لديه جدول تطعيمات بالفعل"));

        var vaccines = Utility.ChildVaccines(null, childId);

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        child.IsNormal = true;

        await unitOfWork.VaccineRepo.AddRangeAsync(vaccines);
        unitOfWork.ChildRepo.Update(child);


        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم اضافه جدول التطعيمات حاول مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم اضافه جدول التطعيمات للطفل");

        return Ok(response);
    }
}
