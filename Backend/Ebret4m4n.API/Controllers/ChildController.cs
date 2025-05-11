using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.API.ChildBaseVaccines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Contracts;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.API.Utilites;
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles ="parent")]
[ApiController]
public class ChildController(IUnitOfWork unitOfWork) : ControllerBase
{

    [AllowAnonymous]
    [HttpGet("child-base-vaccines")]
    public IActionResult GetChildVaccines()
    {
        try
        {
            var vaccines = Utility.ReadBaseVaccineFromJson();
            var response = GeneralResponse<List<BaseVaccine>>.SuccessResponse(vaccines);
            return Ok(response);
        }
        catch(Exception ex)
        {
            return BadRequest(GeneralResponse<string>.FailureResponse(ex.Message));
        }
        
    }

    [HttpGet("{id}/child")]
    public async Task<IActionResult> GetChild(string id)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(e => e.Id == id, false, ["Vaccines", "Transaction"]);

        if (child is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد طفل مسجل بهذا الرقم : {id}"));

        if (child.Transaction is null || !child.Transaction.Paid)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم دفع رسوم تسجيل الطفل"));

        var childDto = child.Adapt<ChildDto>();

        var response = GeneralResponse<ChildDto>.SuccessResponse(childDto);

        return Ok(response);
    }

    [HttpGet("{id}/child-data")]
    public async Task<IActionResult> ChildData(string id)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(child => child.Id == id, false, ["HealthReportFiles"]);

        if (child is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد طفل مسجل بهذا الرقم : {id}"));

        var childDto = child.Adapt<ChildDataDto>();

        var response = GeneralResponse<ChildDataDto>.SuccessResponse(childDto);

        return Ok(response);
    }

    [HttpGet("children")]
    public async Task<IActionResult> GetChildren()
    {
        var parentId = User.FindFirst("id")!.Value;

        var children =
            await unitOfWork.ChildRepo.FindByCondition(Child => Child.UserId == parentId, false, ["Vaccines"])
            .ToListAsync() ?? [];

        var childrenDtos = children.Adapt<List<ChildrenDto>>();

        var response = GeneralResponse<List<ChildrenDto>>.SuccessResponse(childrenDtos);

        return Ok(response);
    }

    [HttpPost("child-add")]
    public async Task<IActionResult> AddChild([FromForm]AddChildDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(
                GeneralResponse<object>.FailureResponse(ModelState));

        if (dto.BirthDate > DateTime.UtcNow)
            return BadRequest(GeneralResponse<string>.FailureResponse("لا يمكن اختيار هذا التاريخ اكبر من التارخ الحالي"));

        var parentId = User.FindFirst("id")!.Value;

        var checkUniqNameIdentifier =
            await unitOfWork.ChildRepo.FindAsync(C => C.Id == dto.Id, false);

        if (checkUniqNameIdentifier != null)
            return BadRequest(GeneralResponse<string>.FailureResponse("من فضلك ادخل رقم الطفل القومي الصحيح"));

        var child = (dto,parentId).Adapt<Child>();

        if(dto.ReportFiles == null && dto.PatientHistory == null)
        {
            var vaccines = Utility.ChildVaccines(dto.TakedVaccines, child.Id);
            await unitOfWork.VaccineRepo.AddRangeAsync(vaccines);
        }

        if (dto.ReportFiles != null) 
        {
            var childReports = Utility.SaveReportFiles(dto.ReportFiles, dto.Id);
            await unitOfWork.HealthyReportRepo.AddRangeAsync(childReports);
        }
        
        await unitOfWork.ChildRepo.AddAsync(child);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ الطفل الرجاء المحاوله مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم اضافه الطفل بنجاح");

        return Ok(response);
    }

    [HttpPut("{id}/child-update")]
    public async Task<IActionResult> UpdateChild(string id, [FromForm] UpdateChildDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<object>.FailureResponse(ModelState));

        var child = await unitOfWork.ChildRepo.FindAsync(e => e.Id == id, true);

        if (child is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لايوجد طفل مسجل بهذا الرقم القومي : {id} ")); 

        dto.Adapt(child);

        if(dto.ImageFiles is not null)
        {
            var childHealthFiles = Utility.SaveReportFiles(dto.ImageFiles, id);
            await unitOfWork.HealthyReportRepo.AddRangeAsync(childHealthFiles);
        }

        unitOfWork.ChildRepo.Update(child);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تحديث بيانات هذا الطفل حاول مره اخري"));

        return NoContent();
    }

    [HttpDelete("{childId}/child-remove")]
    public async Task<IActionResult> RemoveChild(string childId)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false);
        if (child is null)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم ايجاد هذا الطفل"));

        unitOfWork.ChildRepo.Remove(child);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حذف هذا الطفل حاول مره اخري"));

        return NoContent();
    }

    [HttpDelete("delete-file")]
    public async Task<IActionResult> RemoveReportFile([FromBody] RemoveFileDto model)
    {
        var file = await unitOfWork.HealthyReportRepo.FindAsync(file => file.FilePath == model.Path, true);

        if (file is null)
        return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم ايجاد هذا الملف"));

        string relativePath = model.Path.TrimStart('/');
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (System.IO.File.Exists(filePath)) 
            System.IO.File.Delete(filePath);

        unitOfWork.HealthyReportRepo.Remove(file);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حذف هذا الملف حاول مره اخري"));

        return NoContent();
    }
}
