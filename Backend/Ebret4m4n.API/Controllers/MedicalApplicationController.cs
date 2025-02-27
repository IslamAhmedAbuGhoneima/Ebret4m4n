using Ebret4m4n.Shared.DTOs.JobApplicationsDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Contracts;
using Mapster;


namespace Ebret4m4n.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalApplicationController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [Authorize]
        [HttpPost("medical-apply")]
        public async Task<IActionResult> ApplyForMedicalRole(JobApplicationDto model)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var application = (model, userId).Adapt<JobApplications>();

            await unitOfWork.JobApplicationRepo.AddAsync(application);
            int result = await unitOfWork.SaveAsync();

            if (result == 0)
                throw new BadRequestException("لم يتم ارسال الطلب حاول مره اخري");

            return Ok(new { Message = "تم حفظ الطلب بنجاح برجاء انتظار الرد" });
        }
    }
}
