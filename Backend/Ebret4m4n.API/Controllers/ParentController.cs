using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.ParentDtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public ParentController(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    [Authorize]
    [HttpPost("appointment-book")]
    public async Task<IActionResult> AppointmentBook([FromBody] AddAppointmentDto model)
    {
        var parentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var user = await _userManager.Users
            .Include(U => U.HealthCareCenter)
            .Include(U => U.Children.Where(c => c.Id == model.ChildId && c.UserId == parentId))
            .FirstOrDefaultAsync(U => U.Id == parentId);


        if (user?.Children is null)
            throw new BadRequestException($"you do not have children with {model.ChildId} to reserve an appointment");

        if (user?.HealthCareCenterId == null)
            throw new NotFoundBadRequest("the current user dose not belonge to any healthy care center ");

        var appointment = (model, user.HealthCareCenter, parentId).Adapt<Appointment>();

        await _unitOfWork.AppointmentRepo.AddAsync(appointment);
        var result = await _unitOfWork.SaveAsync();

        if (result == 0)
            throw new BadRequestException("Your reservation was not saved, please try again");


        string childName = user.Children.FirstOrDefault()!.Name;

        var appointmentDto = (appointment, childName).Adapt<AppointmentDto>();

        return Ok(appointmentDto);
    }

    [Authorize]
    [HttpPost("{id:guid}/appointment-cancle")]
    public async Task<IActionResult> AppointmentCancle(Guid id)
    {
        var appointment = await _unitOfWork.AppointmentRepo.FindAsync(a => a.Id == id, true);

        if (appointment == null)
            throw new NotFoundBadRequest("there is no appointment for this child");

        appointment.Status = BookStatus.Cancelled;

        _unitOfWork.AppointmentRepo.Update(appointment);
        await _unitOfWork.SaveAsync();

        return Ok(new { Message = "You canclled your appointment" });
    }
}
