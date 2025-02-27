using Ebret4m4n.Shared.DTOs.JobApplicationsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Contracts;
using Mapster;

namespace Ebret4m4n.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class MinistryHealthAdminController
        (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager): ControllerBase
    {
        [HttpGet("{position:alpha}/applied-postions")]
        public IActionResult GetAppliedPostions(string position)
        {
            if (Enum.TryParse(position, out JopPosition pos) == false)
                throw new BadRequestException("لم يتم العثور علي نتيجه");

            var positions = unitOfWork.JobApplicationRepo
                                .FindByCondition(j => j.JobPosition == pos, false, ["User"]);


            if (positions == null)
                throw new NotFoundException("لا يوجد تقديمات لهذا المنصب");

            var postionsDto = positions.Adapt<List<JobPositionRequestsDto>>();

            return Ok(postionsDto);

        }

        [HttpPost("{id:guid}/approve-postion")]
        public async Task<IActionResult> ApprovePostion(Guid id, [FromBody] ApprovePositionRequest model)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var jobPostion = await unitOfWork.JobApplicationRepo.FindAsync(p => p.JobId == id, true);
            var healthCare = await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == model.HealthCareId, false);

            if (healthCare == null || jobPostion == null)
                throw new NotFoundBadRequest("هذه البيانات غير موجوده او لم يتم تسجيل احد بها");

            jobPostion.JobStatus = ApplicationStatus.Accepted;
            unitOfWork.JobApplicationRepo.Update(jobPostion);

            var user = await userManager.FindByIdAsync(jobPostion.UserId);
            if (user == null)
                throw new NotFoundBadRequest("هذا الحساب غير موجود او تم حذف الحساب لهذاالشخص");

            var medicalNumber = jobPostion.MedicalNumber;
            var staffRecord = (user, healthCare, medicalNumber).Adapt<MedicalStaff>();
            staffRecord.StaffRole = jobPostion.JobPosition.ToString();

            await unitOfWork.StaffRepository.AddAsync(staffRecord);

            var result = await unitOfWork.SaveAsync();
            var roleResult = await userManager.AddToRoleAsync(user, staffRecord.StaffRole);


            if (result == 0 || !roleResult.Succeeded) 
                throw new BadRequestException("لم يتم اسناد الدور للمستخدم حاول مره اخري");

            return Ok(new {Meessage = $"{user.UserName}تم اسناد الوضيف للمستخدم"});
        }
    }
}
