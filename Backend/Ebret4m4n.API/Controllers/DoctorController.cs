using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.API.ChildBaseVaccines;
using System.Text.Json;
using Ebret4m4n.Entities.Models;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles ="doctor")]
[ApiController]
public class DoctorController
    (IUnitOfWork unitOfWork): ControllerBase
{
    [HttpGet("children-disease")]
    public IActionResult GetChildrenWithDisease()
    {
        var doctorHcCenterId = User.FindFirst("healthCareId")!.Value;

        var children =
             unitOfWork.ChildRepo.FindByCondition(
                c => c.PatientHistory != null &&
                c.User.HealthCareCenterId.ToString() == doctorHcCenterId, false, ["User"]).ToList();


        var childrenDto = children.Adapt<List<ChildDto>>();
        var response = new GeneralResponse<List<ChildDto>>(StatusCodes.Status200OK, childrenDto);

        return Ok(response);
    }

    [HttpGet("{childId}/child-data")]
    public async Task<IActionResult> GetChildData(string childId)
    {
        var child = 
            await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false, ["HealthReportFiles"]);

        var childDto = child.Adapt<ChildDto>();

        var response = new GeneralResponse<ChildDto>(StatusCodes.Status302Found, childDto);

        return Ok(response);
    }

    [HttpPost("{childId}/suspend")]
    public async Task<IActionResult> SuspendChildVaccine(string childId)
    {
        var child = 
            await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false);

        child.IsNoramal = false;

        unitOfWork.ChildRepo.Update(child);
        await unitOfWork.SaveAsync();

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم تاجيل اللقاحات لهذا الطفل");

        return Ok(response);
    }

    [HttpGet("children-suspended")]
    public IActionResult SuspendedChildren()
    {
        var children = unitOfWork.ChildRepo.FindByCondition(children => children.IsNoramal == false, false)
            .Select(children => children.Name).ToList();

        if (children is null)
            throw new NotFoundException("لا يوجد اطفال مؤجلين حتي الان");

        var response = new GeneralResponse<List<string>>(StatusCodes.Status200OK, children);
        return Ok(response);
    }

    [HttpPost("{childId}/add-normal-vaccine")]
    public async Task<IActionResult> AddNormalVacinnes(string childId)
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

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        await unitOfWork.VaccineRepo.AddRangeAsync(vaccines);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم اضافه جدول التطعيمات حاول مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه جدول التطعيمات للطفل");

        return Ok(response);
    }
}
