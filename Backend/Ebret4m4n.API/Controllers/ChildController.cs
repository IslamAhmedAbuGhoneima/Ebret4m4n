using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using Mapster;
using Ebret4m4n.Contracts;
using Ebret4m4n.Shared.DTOs;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ChildController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("{id}/child")]
    public async Task<IActionResult> GetChild(string id)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(e => e.Id == id, false, ["Vaccines"]);

        if (child is null)
            throw new NotFoundBadRequest($"Child with {id} Not found");

        var childDto = child.Adapt<ChildDto>();

        var response = new GeneralResponse<ChildDto>(StatusCodes.Status200OK,childDto);

        return Ok(response);
    }

    [HttpGet("children")]
    public async Task<IActionResult> GetChildren()
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var children = 
            await unitOfWork.ChildRepo.FindByCondition(Child => Child.UserId == parentId, false).ToListAsync();

        if (children is null)
            throw new NotFoundBadRequest("you don not have any child");


        var childrenDtos = children.Adapt<List<ChildDto>>();

        var response = new GeneralResponse<List<ChildDto>>(StatusCodes.Status200OK, childrenDtos);

        return Ok(response);
    }

    [HttpPost("child-add")]
    public async Task<IActionResult> AddChild([FromBody]AddChildDto dto)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var checkUniqNameIdentifier =
            await unitOfWork.ChildRepo.FindAsync(C => C.Id == dto.Id, false);

        if (checkUniqNameIdentifier != null)
            throw new BadRequestException("من فضلك ادخل رقم الطفل القومي الصحيح");

        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


        var child = (dto,parentId).Adapt<Child>();

        if(dto.healthReportFiles == null && dto.PatientHistory == null)
        {
            var vaccines = ReadVaccinesFromJsonFile(dto.vaccines, child.Id);

            await unitOfWork.VaccineRepo.AddRangeAsync(vaccines);
        }

        if (dto.healthReportFiles != null) 
        {
            var childReports = SaveReportFiles(dto.healthReportFiles, dto.Id);
            await unitOfWork.HealthyReportRepo.AddRangeAsync(childReports);
        }

        await unitOfWork.ChildRepo.AddAsync(child);

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حفظ الطفل الرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه الطفل بنجاح");

        return Ok(response);
    }

    [HttpPut("{id}/child-update")]
    public async Task<IActionResult> UpdateChild(string id, [FromBody] UpdateChildDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var child = await unitOfWork.ChildRepo.FindAsync(e => e.Id == id, true);

        if (child is null)
            throw new NotFoundBadRequest($"Child with {id} Not found"); 

        dto.Adapt(child);

        if(dto.ImageFiles is not null)
        {
            var childHealthFiles = SaveReportFiles(dto.ImageFiles, id);
            await unitOfWork.HealthyReportRepo.AddRangeAsync(childHealthFiles);
        }

        unitOfWork.ChildRepo.Update(child);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم تحديث بيانات هذا الطفل");

        return NoContent();
    }

    [HttpDelete("{childId:alpha}/child-remove")]
    public async Task<IActionResult> RemoveChild(string childId)
    {
        var child = await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false);
        if (child is null)
            throw new BadRequestException("لم يتم ايجاد هذا الطفل");

        unitOfWork.ChildRepo.Remove(child);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم حذف هذا الطفل");

        return NoContent();
    }


    private List<HealthReportFile>? SaveReportFiles(List<IFormFile> imageFiles, string childId)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "ChildReports");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        
        var healthReports = new List<HealthReportFile>();

        foreach (var file in imageFiles)
        {
            if (file.Length <= 5120)
            {
                var fileName = Guid.NewGuid().ToString();
                var fileExtenstion = Path.GetExtension(file.FileName);

                var reportPath = Path.Combine(path, fileName, fileExtenstion);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                healthReports.Add(new HealthReportFile
                {
                    FilePath = Path.Combine($"/ChildReports/{fileName}{fileExtenstion}"),
                    ChildId = childId
                });
            }
            else
                throw new FileLoadException("file size to large");
        }
        return healthReports;
    }

    private List<Vaccine> ReadVaccinesFromJsonFile(List<string>? childVaccines, string childId)
    {
        string path =
                Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "vaccines.json");

        if (!Path.Exists(path))
            throw new FileNotFoundException("لم يتم استرجاع القاحات الرجاء التواصل مع الدعم الفني");

        using var strem = new FileStream(path, FileMode.Open);
        var baseVaccines = JsonSerializer.Deserialize<List<BaseVaccine>>(strem);

        if(baseVaccines == null)
            throw new BadRequestException("حدث خطا ما اثناء تسجيل الطفل الرجاء الاتصال بالدعم الفني للمساعده");

        var vaccines = baseVaccines.Adapt<List<Vaccine>>();

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        if(childVaccines is not null)
        {
            foreach (var vaccineName in childVaccines)
            {
                var vaccine = vaccines.FirstOrDefault(v => v.Name == vaccineName);
            }
        }
        return vaccines;
    }
}
