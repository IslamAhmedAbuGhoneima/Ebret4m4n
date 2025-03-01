using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "cityAdmin")]
[ApiController]
public class CityAdminController : ControllerBase
{
    [HttpGet("healthcareCenter-village")]
    public IActionResult GetHealthCareInVillage()
    {
        return Ok();
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    public IActionResult HealthCareDetails(Guid id)
    {
        // oraginzer info
        // Doctor info
        // inventory info
        return Ok();
    }

    [HttpGet("complaints")]
    public IActionResult Complaints()
    {
        return Ok();
    }

    [HttpGet("{id:guid}/complaint")]
    public IActionResult Complaint(Guid id)
    {
        // user who send complaint and complaint
        return Ok();
    }

    [HttpPost("{complaintId:guid}/handle-complaint")]
    public IActionResult HandleComplaint(Guid complaintId)
    {
        return Ok();
    }


    [HttpPost("{vaccineId:guid}/send-vaccine")]
    public IActionResult SendVaccine(Guid vaccineId)
    {
        return Ok();
    } 
}
