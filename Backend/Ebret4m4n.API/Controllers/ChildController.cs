using Ebret4m4n.API.ChildBaseVaccines;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;


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

    [HttpGet("{id}/Git-Child")]
    public async Task<IActionResult> GetById(string id)
    {
        var child = await _unitOfWork.ChildRepo.FindAsync(e => e.Id == id, true);

        if (child is null)
            return UnprocessableEntity($"Child with {id} Not found");

        ChildDto childDto = new()
        {
            Id = child.Id,
            Name = child.Name,
            AgeInMonth = child.AgeInMonth,
            BirthDate = child.BirthDate,
            Weight = child.Weight,
            Gender = child.Gender,
            PatientHistory = child.PatientHistory,
        };

        return Ok(childDto);
    }
    [Authorize]
    [HttpGet("Get-All-Child-ForParent")]
    public async Task<IActionResult> GetAllChildForParent()
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        ICollection<Child> children = _unitOfWork.ChildRepo.FindByCondition(Child => Child.UserId == parentId, true).ToList();

        if (children is null)
            return UnprocessableEntity("No child found for this parent");

        List<ChildDto> childDtos = new();

        foreach (var child in children)
        {
            childDtos.Add(new ChildDto
            {
                Id = child.Id,
                Name = child.Name,
                AgeInMonth = child.AgeInMonth,
                BirthDate = child.BirthDate,
                Weight = child.Weight
            });
        }
        return Ok(childDtos);
    }
    [Authorize]
    [HttpPost("child-add")]
    public async Task<IActionResult> AddChild([FromBody]AddChildDto dto)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        var child = new Child
        {
            Id = dto.Id,
            Name = dto.Name,
            BirthDate = dto.BirthDate,
            Weight = dto.Weight,
            Gender = dto.Gender,
            UserId = parentId
        };

        if (dto.healthReportFiles != null) 
        {
            var childReports = SaveReportFiles(dto.healthReportFiles, dto.Id);
            await _unitOfWork.HealthyReportRepo.AddRangeAsync(childReports);
        }

        if (dto.vaccines != null) 
        {
            string path = 
                Path.Combine(Directory.GetCurrentDirectory(), "ChildBaseVaccines", "Vaccines.json");

            using (var strem = new FileStream(path, FileMode.Open))
            {
                var baseVaccines = JsonSerializer.Deserialize<List<BaseVaccine>>(strem);
                var childVaccineList = new List<Vaccine>();
                
                foreach(var vac in dto.vaccines)
                {
                    var baseVaccine =  baseVaccines.FirstOrDefault(v => v.Name == vac.Name);
                    var vaccine = new Vaccine
                    {
                        Name = baseVaccine.Name,
                        DocesRequired = baseVaccine.DocesRequired,
                        DocesTaken = baseVaccine.DocesRequired,
                        IsTaken = true,
                        ChildAge = baseVaccine.ChildAge
                    };
                    childVaccineList.Add(vaccine);
                }

                await _unitOfWork.VaccineRepo.AddRangeAsync(childVaccineList);
            }
        }

        await _unitOfWork.ChildRepo.AddAsync(child);

        await _unitOfWork.SaveAsync();

        return Ok();
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

        child.Name = dto.Name;
        child.Weight = dto.Weight;
        child.PatientHistory = dto.PatientHistory;

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
}
