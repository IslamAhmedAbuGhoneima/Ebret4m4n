using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.API.Utilites;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "parent")]
[ApiController]
public class PaymentController
    (IUnitOfWork unitOfWork) : ControllerBase

{
    [HttpPost("{childId}/process-payment")]
    public async Task<IActionResult> ProccessPayment(string childId)
    {
        var parentId = User.FindFirst("id")!.Value;
        var child = await unitOfWork.ChildRepo.FindAsync(child => child.Id == childId, false, ["User"]);

        if (child is null)
            return BadRequest(GeneralResponse<string>.FailureResponse("لا يوجد طفل مسجل بهذا الرقم"));

        var session = await Utility.CreateSessionPayment(parentId, child.User.Email!, childId, child.Name);

        var response = GeneralResponse<string>.SuccessResponse(session.Url);

        return Ok(response);
    }
}
