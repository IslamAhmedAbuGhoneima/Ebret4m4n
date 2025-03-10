﻿using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Mapster;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "parent")]
[ApiController]
public class ParentController
    (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("children-reservations")]
    public IActionResult ParentReservations()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var appointments =
            unitOfWork.AppointmentRepo.FindByCondition(
                a => a.UserId == userId , false, ["Child", "Vaccine"]);

        var userReservationsDto = appointments.Adapt<List<UserReservationDto>>();

        var response = new GeneralResponse<List<UserReservationDto>>(StatusCodes.Status200OK, userReservationsDto);

        return Ok(response);
    }

    [HttpPost("appointment-book")]
    public async Task<IActionResult> AppointmentBook([FromBody] AddAppointmentDto model)
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var user = await userManager.Users
            .Include(U => U.HealthCareCenter)
            .Include(U => U.Children.Where(c => c.Id == model.ChildId && c.UserId == parentId))
            .FirstOrDefaultAsync(U => U.Id == parentId);


        if (user?.Children is null)
            throw new BadRequestException($"you do not have children with {model.ChildId} to reserve an appointment");

        if (user?.HealthCareCenterId == null)
            throw new NotFoundBadRequest("the current user dose not belonge to any healthy care center ");

        var appointment = (model, user.HealthCareCenter, parentId).Adapt<Appointment>();

        await unitOfWork.AppointmentRepo.AddAsync(appointment);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("Your reservation was not saved, please try again");


        string childName = user.Children.FirstOrDefault()!.Name;

        var appointmentDto = (appointment, childName).Adapt<AppointmentDto>();

        var response = new GeneralResponse<AppointmentDto>(StatusCodes.Status200OK, appointmentDto);

        return Ok(response);
    }

    [HttpDelete("{id:guid}/appointment-cancle")]
    public async Task<IActionResult> AppointmentCancle(Guid id)
    {
        var appointment = await unitOfWork.AppointmentRepo.FindAsync(a => a.Id == id, true);

        if (appointment == null)
            throw new NotFoundBadRequest("لا يوجد موعد خاص بهذا المستخدم");

        unitOfWork.AppointmentRepo.Remove(appointment);
        var result = await unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("لم يتم الغاء المعاد حاول مره اخري");

        return NoContent();
    }

    [HttpGet("Submit-Complaint")]
    public async Task<IActionResult> SubmitComplaint()
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
        .FindAsync(hc => hc.Users.Any(u => u.Id == parentId), false, "Users");
        

        var x = userManager.Users.Include(c => c.HealthCareCenter)
            .FirstOrDefault(c => c.Id == parentId);


        return healthCareCenter != null ?
           Ok(healthCareCenter.HealthCareCenterName)
         : NotFound("غير مسجل بوحدة صحية");
    }

    [HttpPost("Send-Complaint")]
    public async Task<IActionResult> SendComplaint(string message)
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var complaint = new Complaint
        {
            Message = message,
            UserId = parentId
        };

        await unitOfWork.ComplaintRepo.AddAsync(complaint);
        await unitOfWork.SaveAsync();
        return Ok(complaint);
    }
}
