using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Entities.Models;
using Mapster;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Azure;
using Ebret4m4n.Shared.DTOs.VaccinDto;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizerController(IUnitOfWork unitOfWork) : ControllerBase
{

	[HttpGet("{id}/search_by_id")]
	public async Task<IActionResult> GetById(string id)
	{
		var child=await unitOfWork.ChildRepo.FindAsync(c=>c.Id==id,false, ["Vaccines", "HealthReportFiles"]);
		if (child == null)
			throw new NotFoundBadRequest("لا يوجد طفل مسجل بهذا الرقم");

		var childDtos = child.Adapt<ChildDto>();

		var response = new GeneralResponse<ChildDto>(StatusCodes.Status200OK, childDtos);

		return Ok(response);
	}

	[HttpPost("update_vaccine_statues")]
	public async Task<IActionResult> UpdateVaccineStatues(List<UpdateVaccineDto> Children)
	{

		foreach(var child in Children)
		{
			var Child = await unitOfWork.ChildRepo.FindAsync(c => c.Id == child.Id, false, ["Vaccines"]);
			if (child == null)
				throw new NotFoundBadRequest("لا يوجد طفل مسجل بهذا الرقم");
			var vaccine = Child.Vaccines.FirstOrDefault(v => v.Name == child.VaccineName);
			if (vaccine == null)
				throw new NotFoundBadRequest("لا يوجد تطعيم بهذا الاسم");
			vaccine.IsTaken = true;
			unitOfWork.ChildRepo.Update(Child);
		}


		var result = await unitOfWork.SaveAsync();
		if(result==0)
			throw new BadRequestException("لم يتم تحديث حالة التطعيم");

		return Ok();
	}

	[HttpPost("{id}/post_Appointment")]
	public async Task<IActionResult>PostponementAppointment(Guid id)
	{
		var appointment =await unitOfWork.AppointmentRepo.FindAsync(a => a.Id == id, false);
		if (appointment == null)
			return NotFound("الموعد غير موجود");

		appointment.Date=appointment.Date.AddDays(7);

		unitOfWork.AppointmentRepo.Update(appointment);
		var result = await unitOfWork.SaveAsync();
		
		if( result==0)
			throw new BadRequestException("لم يتم تاجيل الميعاد");

		return Ok();

	}

	[HttpGet("upcoming_Children")]
	public async Task<IActionResult>UpcomingChildren()
	{
		var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
		var children =
			await unitOfWork.ChildRepo.FindByCondition(Child => Child.UserId == parentId, false, ["Vaccines"]).ToListAsync();

		List<Child> childrenList = new List<Child>();

		foreach (var child in children)
		{
			var date = child.Appointments.FirstOrDefault(e =>e.Date==DateTime.Now);
			if (date!=null)
			{
				childrenList.Add(child);
			}
			
		}

		if(childrenList.Count>0)
			throw new BadRequestException("لا يوجد اطفال قادمون اليوم");

		var childrenDtos = children.Adapt<List<ChildDto>>();
		var response = new GeneralResponse<List<ChildDto>>(StatusCodes.Status200OK, childrenDtos);

		return Ok(response);

	}

	[HttpGet("get_inventory")]
	public async Task<IActionResult> GetInventory()
	{
		var organizerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
		var organizer =await unitOfWork.StaffRepo.FindAsync(organizer => organizer.UserId == organizerId, false);
		if(organizer == null)
			throw new NotFoundBadRequest("هذه الوحدة لا يوجد بها ممرض");

		var inventory =
			await unitOfWork.InventoryRepo.FindByCondition(e => e.HealthCareCenter.HealthCareCenterName == organizer.HealthCareCenterName, false).ToListAsync();

		if(inventory == null)
			throw new NotFoundBadRequest("لم يتم انشاء مخزن ل هذه الوحدة");

		var inventoryDto = inventory.Adapt<Inventory>();
		var response = new GeneralResponse<Inventory>(StatusCodes.Status200OK, inventoryDto);
		return Ok(response);
	}
}

