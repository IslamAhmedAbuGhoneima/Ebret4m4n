using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Ebret4m4n.Entities.Exceptions;
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

        var children =
             unitOfWork.ChildRepo.FindByCondition(
                c => c.PatientHistory != null &&
                (c.Transaction != null && c.Transaction.Paid == true) &&
                c.User.HealthCareCenterId.ToString() == doctorHcCenterId, false, ["User", "Transaction"]).ToList();


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

        child.IsNormal = false;

        unitOfWork.ChildRepo.Update(child);
        int result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم تاجيل اللقاحات لهذا الطفل حاول مره اخري");

        await emailSender.SendEmailAsync(child.User.Email!, "تاجيل التطعيمات", $"<p>بناء علي التحاليل المقدمه تم تاجيل التطعيم لطفلك : {child.Name}</p>");

        var response = GeneralResponse<string>.SuccessResponse("تم تاجيل اللقاحات لهذا الطفل");

        return Ok(response);
    }

    [HttpGet("children-suspended")]
    public IActionResult SuspendedChildren()
    {
        var children = unitOfWork.ChildRepo.FindByCondition(children => children.IsNormal == false, false)
            .Select(children => children.Name).ToList();

        if (children is null)
            throw new NotFoundException("لا يوجد اطفال مؤجلين حتي الان");

        var response = GeneralResponse<List<string>>.SuccessResponse(children);
        return Ok(response);
    }

    [HttpPost("{childId}/add-normal-vaccine")]
    public async Task<IActionResult> AddNormalVacinnes(string childId)
    {
        var vaccines = Utility.ChildVaccines(null, childId);

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        await unitOfWork.VaccineRepo.AddRangeAsync(vaccines);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم اضافه جدول التطعيمات حاول مره اخري");

        var response = GeneralResponse<string>.SuccessResponse("تم اضافه جدول التطعيمات للطفل");

        return Ok(response);
    }
}
