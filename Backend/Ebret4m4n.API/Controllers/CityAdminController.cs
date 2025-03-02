using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "cityAdmin")]
[ApiController]
public class CityAdminController : ControllerBase
{
    IUnitOfWork UnitOfWork;
    //ApplicantPosition position = new ApplicantPosition();

    public CityAdminController(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }


    [HttpGet("healthcareCenter-village")]
    public async Task<IActionResult> GetHealthCareInVillageAsync()
    {
        var city = User.FindFirst("city")!.Value;

        //var admin = await UnitOfWork.cityAdminStaffRepository.FindAsync(c=>c.UserId==adminId,false);

        var healthCareCenters = await UnitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.City == city, false)
            .Select(hc => hc.Village)
            .ToListAsync();

        return Ok(healthCareCenters);
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    public IActionResult HealthCareDetails(Guid id)
    {
        
        var healthCareCenter = UnitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.HealthCareCenterId == id, false).FirstOrDefault();

        if (healthCareCenter is null)
            return NotFound();
        // oraginzer info
        //الاسم - email-city-position

        var organizer = UnitOfWork.MedicalApplicationRepo
            .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
           && m.ApplicantPosition == ApplicantPosition.Organizer, false).FirstOrDefault();


        //Doctor info

        var doctor = UnitOfWork.MedicalApplicationRepo
            .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
            && m.ApplicantPosition == ApplicantPosition.Doctor, false).FirstOrDefault();

        // inventory info

        //var inventory = UnitOfWork.InventoryRepo
        //    .FindByCondition(i => i.HealthCareCenterId == id, false).FirstOrDefault();
        return Ok();
    }

    [HttpGet("complaints")]
    public async Task<IActionResult> Complaints()
    {
        var city = User.FindFirst("city")!.Value;

       // var admin = await UnitOfWork.cityAdminStaffRepository.FindAsync(c => c.UserId == adminId, false);

        var complaints = await UnitOfWork.ComplaintRepo
            .FindByCondition(c => c.User.City == city, false)
            .ToListAsync();

        if (complaints.Count == 0)
            return NotFound("لا توجد شكاوى");

        return Ok(complaints);
    }

    [HttpGet("{id:guid}/complaint")]
    public IActionResult Complaint(Guid id)
    {
        var complaint = UnitOfWork.ComplaintRepo
            .FindByCondition(c => c.Id == id, false).FirstOrDefault();
        // user who send complaint and complaint
        if (complaint is null)
            return NotFound("لا توجد شكاوى ");

        return Ok(complaint);
    }

    [HttpPost("{complaintId:guid}/handle-complaint")]
    public IActionResult HandleComplaint(Guid complaintId)
    {

        return Ok();
    }


    [HttpPost("{vaccineId:guid}/send-vaccine")]
    public IActionResult SendVaccine(Guid vaccineId , int amount)
    {



        return Ok();
    } 
}
