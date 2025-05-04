using Ebret4m4n.Shared.DTOs.AppointmentDtos;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.VaccinDto;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Ebret4m4n.API.Utilites;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "organizer")]
[ApiController]
public class OrganizerController(IUnitOfWork unitOfWork) : ControllerBase
{

	[HttpGet("{id}/search_by_id")]
	public async Task<IActionResult> GetById(string id)
	{
		var child = await unitOfWork.ChildRepo.FindAsync(c => c.Id == id, false, ["Vaccines", "HealthReportFiles"]);

		if (child == null)
			return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد طفل مسجل بهذا الرقم"));

		var childDtos = child.Adapt<ChildDto>();

		var response = GeneralResponse<ChildDto>.SuccessResponse(childDtos);

		return Ok(response);
	}

	[HttpPost("update_vaccine_statues")]
	public async Task<IActionResult> UpdateVaccineStatues(List<UpdateVaccineDto> model)
	{
        if (!ModelState.IsValid)
			return UnprocessableEntity( GeneralResponse<string>.FailureResponse("تاكد من ان جميع المدخلات صحيحه"));

        var orgnizerHCId = User.FindFirst("healthCareId")!.Value;

		await unitOfWork.BeginTransactionAsync();

		try
		{
            var childrenIds = model.Select(c => c.Id).ToList();

            var children = unitOfWork.ChildRepo.FindByCondition(c => childrenIds.Contains(c.Id), true, ["Vaccines"])
                .ToList();

            var inventory = unitOfWork.InventoryRepo.FindByCondition(inv => inv.HealthCareCenterId.ToString() == orgnizerHCId, true);


            List<string> notFoundChildren = [];

            foreach (var data in model)
            {
                var child = children.FirstOrDefault(c => c.Id == data.Id);

                if (child == null)
                {
                    notFoundChildren.Add($":لا يوجد طفل مسجل بهذا الرقم {data.Id}");
                    continue;
                }

                if (child.Vaccines == null)
                {
                    notFoundChildren.Add($" :لا يوجد تطعيمات مسجله لهذا الطفل {data.Id}");
                    continue;
                }

                var vaccine = child.Vaccines.FirstOrDefault(v => v.Name == data.VaccineName);

				if (vaccine is null)
					return NotFound(GeneralResponse<string>.FailureResponse($"لايوجد تطيعم بهذا الاسم: {data.VaccineName}"));

                vaccine.IsTaken = true;

                unitOfWork.ChildRepo.Update(child);
            }

            if (notFoundChildren.Count > 0)
                return BadRequest(notFoundChildren);

            await UpdateOrgnizerInventory(model, inventory);

            var result = await unitOfWork.SaveAsync();

            if (result == 0)
                return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تحديث حالة التطعيم حاول مره اخري"));

			await unitOfWork.CommitTransactionAsync();

            var response = GeneralResponse<string>.SuccessResponse("تم تحديث حاله التطعيم بنجاح");

            return Ok(response);

        }
		catch(Exception ex)
		{
			await unitOfWork.RollbackTransactionAsync();
			return StatusCode(StatusCodes.Status500InternalServerError,  GeneralResponse<string>.FailureResponse($"حدث خطأ ما اثناء تحديث حالة التطعيم : {ex.Message}"));
        }  
	}

	[HttpPost("{id}/postpone_child_appointment_vaccine")]
	public async Task<IActionResult> PostponeChildAppointment(string childId)
	{
		var appointment = await unitOfWork.AppointmentRepo.FindAsync(a => a.ChildId == childId, false);

		if (appointment == null)
			 return NotFound(GeneralResponse<string>.FailureResponse("لا يوجد موعد محجوز لهذا الطفل"));

		appointment.Date = appointment.Date.AddDays(7);

		unitOfWork.AppointmentRepo.Update(appointment);

		var result = await unitOfWork.SaveAsync();

		if (result == 0) 
			return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم تاجيل الموعد حاول مره اخري"));

		var response = GeneralResponse<string>.SuccessResponse("تم تاجيل الميعاد بنجاح");

        return Ok(response);
	}

	[HttpGet("coming_children")]
	public IActionResult ComingChildren()
	{
		var orgnizerHCId = User.FindFirst("healthCareId")!.Value;

		var children = unitOfWork.AppointmentRepo.FindByCondition(a => a.Date == DateTime.UtcNow && a.User.HealthCareCenterId.ToString() == orgnizerHCId, false, ["User", "Child"])
			.Select(a => new { a.Child.Name, a.User.FirstName, a.User.LastName, a.ChildId, a.VaccineName })
			.ToList() ?? [];
			
		var childrenDtos = children.Adapt<List<ComingChildrenDto>>();

		var response = GeneralResponse<List<ComingChildrenDto>>.SuccessResponse(childrenDtos);

		return Ok(response);
	}

	private async Task UpdateOrgnizerInventory(List<UpdateVaccineDto> vaccines, IQueryable<Inventory> inventories)
	{
        var baseVaccines = Utility.ReadBaseVaccineFromJson();

		foreach (var vaccine in vaccines)
		{
			var vaccineAntigens = baseVaccines.FirstOrDefault(bVacc => bVacc.name == vaccine.VaccineName)!.antigens;
			await inventories.Where(inv => vaccineAntigens.Contains(inv.Antigen))
				.ExecuteUpdateAsync(setter => setter.SetProperty(prop => prop.Amount, e => e.Amount - 1));
		}
    }
}

