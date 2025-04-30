using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.HealthCareDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthCareController(IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("healthCare-locations")]
    public IActionResult HealthCareLocations()
    {
        var allCenters = unitOfWork.HealthCareCenterRepo.FindAll(false);

        var locations = allCenters
            .GroupBy(center => center.Governorate)
            .Select(g => new GovernoratesAndCitiesDto(
                g.Key,
                g.Select(c => c.City).Distinct().ToList()
            )).ToList();

        if (!locations.Any())
            throw new NotFoundBadRequest("لم يتم العثور على وحدات مسجلة لأي محافظة");


        var response =
            new GeneralResponse<List<GovernoratesAndCitiesDto>>(StatusCodes.Status200OK, locations);
        return Ok(response);
    }

    [HttpGet("healthCares")]
    public IActionResult HealthCareCenters([FromQuery] HealthCareSearchParametersDto model)
    {
        var healthcareCenters =
            unitOfWork.HealthCareCenterRepo.FindByCondition(hc => hc.City == model.City && hc.Governorate == model.Governorate, false)
            .Select(hc => hc.Adapt<HealthCaresListDto>())
            .ToList() ?? [];

        var response = new GeneralResponse<List<HealthCaresListDto>>(StatusCodes.Status200OK, healthcareCenters);

        return Ok(response);

    }

    [HttpGet("healthcare-details")]
    [Authorize(Roles = "parent")]
    public async Task<IActionResult> HealthCareDetails()
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var user = await userManager.Users
            .Include(U => U.HealthCareCenter)
            .FirstOrDefaultAsync(U => U.Id == parentId);

        if (user?.HealthCareCenter is null)
            throw new NotFoundBadRequest("هذا المستخدم لا ينتمي الي اي وحده صحيه");

        var healthCareDetailsDto = user.HealthCareCenter.Adapt<HealthCareDetailsDto>();

        var response = new GeneralResponse<HealthCareDetailsDto>(StatusCodes.Status200OK, healthCareDetailsDto);

        return Ok(response);
    }
}
