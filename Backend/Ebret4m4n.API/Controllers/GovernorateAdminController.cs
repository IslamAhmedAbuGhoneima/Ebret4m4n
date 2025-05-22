using Ebret4m4n.Shared.DTOs.AdminsDto.CityAdminDots;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Models;                    
using Microsoft.AspNetCore.Mvc;                                                                 
using Ebret4m4n.Shared.DTOs;                        
using Ebret4m4n.Contracts;                          
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "governorateAdmin")]
[ApiController]
public class GovernorateAdminController
    (IUnitOfWork unitOfWork, 
    UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("cities")]
    public IActionResult GetCities()
    {
        string adminGovernorate = User.FindFirst("governorate")!.Value;

        var cities = unitOfWork.CityAdminStaffRepo
            .FindByCondition(hc => hc.Governorate == adminGovernorate, false)
            .Select(hc => hc.City)
            .Distinct()
            .ToList() ?? [];

        var response = GeneralResponse<List<string>>.SuccessResponse(cities);

        return Ok(response);
    }

    [HttpGet("city-details")]
    public async Task<IActionResult> CityDetails([FromQuery] string cityName)
    {
        var cityAdminStaff =
             await unitOfWork.CityAdminStaffRepo.FindAsync(C => C.City == cityName, false, ["MainInventories", "User", "HealthCareCenters"]);

        if (cityAdminStaff is null)
            return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد ادمن لهذه المدينه"));


        var cityDetails = cityAdminStaff.Adapt<CityRecordDetailsDto>();

        var response = GeneralResponse<CityRecordDetailsDto>.SuccessResponse(cityDetails);

        return Ok(response);
    }

    [HttpGet("city-admins")]
    public IActionResult CityAdmins()
    {
        var governorateAdminId = User.FindFirst("id")!.Value;

        var cityAdmins =
            unitOfWork.CityAdminStaffRepo.FindByCondition(admin => admin.GovernorateAdminId == governorateAdminId, false, "User")
            .Select(admin => admin.Adapt<CityAdminsDto>())
            .ToList() ?? [];

        var response = GeneralResponse<List<CityAdminsDto>>.SuccessResponse(cityAdmins);

        return Ok(response);
    }

    [HttpPost("city-admin-add")]
    public async Task<IActionResult> AddCityAdmin(AddCityAdminDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<string>.FailureResponse("تاكد ان جميع المدخلات صحيحه"));

        var adminGovernorate = User.FindFirst("governorate")!.Value;

        var checkAdminCityExists = await unitOfWork.CityAdminStaffRepo.ExistsAsync(city => city.City == model.City);

        if (checkAdminCityExists)
            return BadRequest(GeneralResponse<string>.FailureResponse("يوجد ادمن لهذه المدينه بالفعل"));

        var governorateAdminId = User.FindFirst("id")!.Value;

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم انشاء هذا المستخدم تاكد من ادخال البيانات بشكل صحيح"));

        await userManager.AddToRoleAsync(user, model.Role);
        
        var cityAdmin = new CityAdminStaff 
        {
            UserId = user.Id,
            GovernorateAdminId = governorateAdminId,
            Governorate = adminGovernorate,
            City = model.City
        };

        await unitOfWork.CityAdminStaffRepo.AddAsync(cityAdmin);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حفظ البيانات الخاصه بهذا المستخدم"));

        var response = GeneralResponse<string>.SuccessResponse("تم اضافه ادمن مدينه بنجاح");

        return Ok(response);
    }

    [HttpGet("{cityAdminId}/city-admin-data")]
    public async Task<IActionResult> GetCityAdminData(string cityAdminId)
    {
        var cityAdmin = await unitOfWork.CityAdminStaffRepo.FindAsync(admin => admin.UserId == cityAdminId, false, ["User"]);

        if (cityAdmin == null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور علي هذاالمستخدم"));

        var cityAdminData = cityAdmin.Adapt<CityAdminDetailsDto>();

        var response = GeneralResponse<CityAdminDetailsDto>.SuccessResponse(cityAdminData);

        return Ok(response);
    }

    [HttpPut("{cityAdminId}/update-city-admin")]
    public async Task<IActionResult> UpdateCityAdmin(string cityAdminId, [FromBody] UpdateCityAdminDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<object>.FailureResponse(ModelState));

        var cityAdmin = await unitOfWork.CityAdminStaffRepo.FindAsync(admin => admin.UserId == cityAdminId, true, ["User"]);

        if (cityAdmin == null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور علي هذا المستخدم"));

        cityAdmin.City = model.City;
        cityAdmin.User.NormalizedEmail = userManager.NormalizeEmail(model.Email);
        model.Adapt(cityAdmin.User);

        var userMangerResult = await userManager.UpdateAsync(cityAdmin.User);

        if(!userMangerResult.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("خطا في البيانات المدخله او الايميل موجود من قبل"));

        unitOfWork.CityAdminStaffRepo.Update(cityAdmin);

        var result = await unitOfWork.SaveAsync();

        if (result == 0) 
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تحديث البيانات حاول مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم تحديث البيانات بنجاح");

        return Ok(response);
    }

    [HttpDelete("{cityAdminId:guid}/remove-admin")]
    public async Task<IActionResult> DeleteCityAdmin(Guid cityAdminId)
    {
        var adminId = cityAdminId.ToString();

        var cityAdmin =
            await unitOfWork.CityAdminStaffRepo.FindAsync(admin => admin.UserId == adminId, false);

        if (cityAdmin == null)
            return NotFound(GeneralResponse<string>.FailureResponse("لم يتم العثور علي هذا المستخدم"));

        var user = await userManager.FindByIdAsync(adminId);

        unitOfWork.CityAdminStaffRepo.Remove(cityAdmin);

        await userManager.RemoveFromRoleAsync(user!, "cityAdmin");

        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم حذف هذا المستخدم حاول مره اخري"));

        var response = GeneralResponse<string>.SuccessResponse("تم حذف هذا المستخدم");

        return Ok(response);
    }
}