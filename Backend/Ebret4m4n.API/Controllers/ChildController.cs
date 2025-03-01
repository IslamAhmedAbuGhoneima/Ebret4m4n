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


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChildController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ChildController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}/child")]
    public async Task<IActionResult> GetChild(string id)
    {
        var child = await _unitOfWork.ChildRepo.FindAsync(e => e.Id == id, false, ["Vaccines"]);

        if (child is null)
            throw new NotFoundBadRequest($"Child with {id} Not found");

        var childDto = child.Adapt<ChildDto>();

        return Ok(childDto);
    }

    [Authorize]
    [HttpGet("children")]
    public async Task<IActionResult> GetChildren()
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var children = 
            await _unitOfWork.ChildRepo.FindByCondition(Child => Child.UserId == parentId, false).ToListAsync();

        if (children is null)
            throw new NotFoundBadRequest("you don not have any child");

        List<ChildDto> childrenDtos = new();

        foreach (var child in children)
        {
            var childDto = child.Adapt<ChildDto>();
            childrenDtos.Add(childDto);
        }

        return Ok(childrenDtos);
    }

    [Authorize]
    [HttpPost("child-add")]
    public async Task<IActionResult> AddChild([FromBody]AddChildDto dto)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var checkUniqNameIdentifier =
            await _unitOfWork.ChildRepo.FindAsync(C => C.Id == dto.Id, false);

        if (checkUniqNameIdentifier != null)
            throw new BadRequestException("من فضلك ادخل رقم الطفل القومي الصحيح");

        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


        var child = (dto,parentId).Adapt<Child>();

        if(dto.healthReportFiles == null && dto.PatientHistory == null)
        {
            var vaccines = ReadFromJsonFile(dto.vaccines, child.Id);

            await _unitOfWork.VaccineRepo.AddRangeAsync(vaccines);
        }

        if (dto.healthReportFiles != null) 
        {
            var childReports = SaveReportFiles(dto.healthReportFiles, dto.Id);
            await _unitOfWork.HealthyReportRepo.AddRangeAsync(childReports);
        }

        await _unitOfWork.ChildRepo.AddAsync(child);

        var result = await _unitOfWork.SaveAsync();


        return Ok(new { Message = "Child created successfully" });
    }

    [Authorize]
    [HttpPut("{id}/child-update")]
    public async Task<IActionResult> UpdateChild(string id, [FromBody] UpdateChildDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var child = await _unitOfWork.ChildRepo.FindAsync(e => e.Id == id, true);

        if (child is null)
            throw new NotFoundBadRequest($"Child with {id} Not found"); 

        dto.Adapt(child);

        if(dto.ImageFiles is not null)
        {
            var childHealthFiles = SaveReportFiles(dto.ImageFiles, id);
            await _unitOfWork.HealthyReportRepo.AddRangeAsync(childHealthFiles);
        }

        _unitOfWork.ChildRepo.Update(child);
        await _unitOfWork.SaveAsync();

        return Ok(new { Message = "Child updated successfully" });
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

    private List<Vaccine> ReadFromJsonFile(List<string>? childVaccines, string childId)
    {
        string path =
                Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "vaccines.json");

        using var strem = new FileStream(path, FileMode.Open);
        var baseVaccines = JsonSerializer.Deserialize<List<BaseVaccine>>(strem);

        if(baseVaccines == null)
        {
            //_logger.LogError("cant serialize the json object to List of baseVaccines");
            throw new BadRequestException("حدث خطا ما اثناء تسجيل الطفل الرجاء الاتصال بالدعم الفني للمساعده");
        }

        var vaccines = baseVaccines.Adapt<List<Vaccine>>();

        foreach (var vaccine in vaccines)
            vaccine.ChildId = childId;

        if(childVaccines is not null)
        {
            foreach (var vaccineName in childVaccines)
            {
                var vaccine = vaccines.FirstOrDefault(v => v.Name == vaccineName);
                vaccine.DocesTaken = vaccine.DocesRequired;
            }
        }
        return vaccines;
    }
}
