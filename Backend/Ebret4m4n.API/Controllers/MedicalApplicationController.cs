using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Contracts;
using Mapster;
using Ebret4m4n.Shared.DTOs.MedicalApplicationsDtos;


namespace Ebret4m4n.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MedicalApplicationController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost("medical-apply")]
        public async Task<IActionResult> ApplyForMedicalRole([FromBody]MedicalApplicationDto model)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var userApplicationExists =
                await unitOfWork.MedicalApplicationRepo.ExistsAsync(job => job.UserId == userId
                && Equals(job.ApplicantPosition, model.Position));

            if (userApplicationExists)
                throw new BadRequestException("تقدم هذا المستخدم بالفعل لهذا المنصب من قبل الرجاء انتظار الرد");

            var application = (model, userId).Adapt<MedicalApplication>();

            await unitOfWork.MedicalApplicationRepo.AddAsync(application);
            int result = await unitOfWork.SaveAsync();

            if (result == 0)
                throw new BadRequestException("لم يتم ارسال الطلب حاول مره اخري");

            return Ok(new { Message = "تم حفظ الطلب بنجاح برجاء انتظار الرد" });
        }

        [HttpPut("medical-apply-update")]
        public async Task<IActionResult> UpdateMedicalRequest([FromBody]MedicalApplicationDto model)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userApplication =
                await unitOfWork.MedicalApplicationRepo.FindAsync(job => job.UserId == userId, false);


            if (userApplication is null)
                throw new NotFoundBadRequest("لم يتم تقديم طلب التحاق بالقطاع الطبي لهذا المستخدم");

            var application = (model, userId).Adapt<MedicalApplication>();

            unitOfWork.MedicalApplicationRepo.Update(application);
            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                throw new BadRequestException("حدث خطا اثناء حفظ البيانات حاول مره اخري");

            return Ok(new { Message = "تم تعديل طلب الالتحاق بنجاح" });
        }


        [HttpDelete("medical-apply-delete")]
        public async Task<IActionResult> DeleteMedicalRequest()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userApplication =
                await unitOfWork.MedicalApplicationRepo.FindAsync(job => job.UserId == userId, false);

            if (userApplication is null)
                throw new NotFoundBadRequest("لم يتم تقديم طلب التحاق بالقطاع الطبي لهذا المستخدم");

            unitOfWork.MedicalApplicationRepo.Remove(userApplication);
            var result = await unitOfWork.SaveAsync();

            if(result == 0)
                throw new BadRequestException("حدث خطا اثناء حفظ البيانات حاول مره اخري");

            return Ok(new { Message = "تم الغاء طلب الالتحاق بنجاح" });
        }

    }
}
