using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Shared.DTOs.ComplaintDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Ebret4m4n.Shared.DTOs.OrderDtos;
using System.Security.Claims;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "cityAdmin")]
[ApiController]
public class CityAdminController
    (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : ControllerBase
{

    [HttpGet("healthcareCenter-village")]
    public async Task<IActionResult> GetHealthCareInVillageAsync()
    {
        var city = User.FindFirst("city")!.Value;

        var healthCareCenters = await unitOfWork.HealthCareCenterRepo
            .FindByCondition(hc => hc.City == city, false)
            .Select(hc => hc.Village)
            .ToListAsync();

        return Ok(healthCareCenters);
    }

    [HttpGet("{id:guid}/healthCareCenter")]
    public async Task<IActionResult> HealthCareDetails(Guid id)
    {
        
        var healthCareCenter = await unitOfWork.HealthCareCenterRepo
            .FindAsync(hc => hc.HealthCareCenterId == id, false);

        if (healthCareCenter is null)
            return NotFound();
        // oraginzer info
        //الاسم - email-city-position

        //var organizer = unitOfWork.MedicalApplicationRepo
        //    .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
        //   && m.ApplicantPosition == ApplicantPosition.Organizer, false).FirstOrDefault();


        //Doctor info

        //var doctor = unitOfWork.MedicalApplicationRepo
        //    .FindByCondition(m => m.HealthCareName == healthCareCenter.HealthCareCenterName
        //    && m.ApplicantPosition == ApplicantPosition.Doctor, false).FirstOrDefault();

        // inventory info

        //var inventory = UnitOfWork.InventoryRepo
        //    .FindByCondition(i => i.HealthCareCenterId == id, false).FirstOrDefault();
        return Ok();
    }


    [HttpPost("medical-postion-add")]
    public async Task<IActionResult> AddMedicalPostion(MedicalStaffDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var healthCare =
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == model.HealthCareCenterId, false);

        if (healthCare is null)
            throw new BadRequestException("لم يتم العثور علي هذه الوحده الصحيه");

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new BadRequestException("لم يتم انشاء هذا المستخدم");

        await userManager.AddToRoleAsync(user, model.StaffRole);

        var medicalStaff = (model, healthCare).Adapt<MedicalStaff>();

        medicalStaff.UserId = user.Id;
            
        await unitOfWork.StaffRepo.AddAsync(medicalStaff);
        var dbResult = await unitOfWork.SaveAsync();

        if (dbResult == 0)
            throw new BadRequestException("لم يتم حفظ الباينات اللرجاء المحاوله مره اخري");

        var response = new GeneralResponse<string>(StatusCodes.Status200OK, "تم اضافه هذا المستخدم بنجاح");

        return Ok(response);
    }

    [HttpGet("complaints")]
    public async Task<IActionResult> Complaints()
    {
        var city = User.FindFirst("city")!.Value;

        var complaints = await unitOfWork.ComplaintRepo
            .FindByCondition(c => c.User.City == city, false, "User")
            .Select(C => new ComplaintsDto(C.User.UserName, C.DateSubmitted))
            .ToListAsync();
            

        if (complaints is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله");

        var response = new GeneralResponse<List<ComplaintsDto>> (StatusCodes.Status200OK, complaints);

        return Ok(response);
    }

    [HttpGet("{id:guid}/complaint")]
    public async Task<IActionResult> Complaint(Guid id)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(c => c.Id == id, false, "User");

        if (complaint is null)
            throw new NotFoundBadRequest("لا توجد شكاوي مسجله لهذا المستخدم");

        var healthCare = 
            await unitOfWork.HealthCareCenterRepo.FindAsync(hc => hc.HealthCareCenterId == complaint.User.HealthCareCenterId, false);

        var complaintDto = (complaint, healthCare).Adapt<ComplaintDto>();

        var response = new GeneralResponse<ComplaintDto>(StatusCodes.Status200OK, complaintDto);

        return Ok(response);
    }

    [HttpPost("{complaintId:guid}/handle-complaint")]
    public async Task<IActionResult> HandleComplaint(Guid complaintId)
    {
        var complaint =
            await unitOfWork.ComplaintRepo.FindAsync(C => C.Id == complaintId, false, "User");

        if (complaint is null)
            throw new NotFoundException("هذه الشكوي غير موجوده او تم حذفها");

        // send email or notfication to user

        return Ok();
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] List<OrderDto> orderItems)
    {

        var AdminId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


        if (orderItems == null)
            return BadRequest("الطلب لا يحتوى على اى عناصر");

        Order order = new Order
        {
            Status = OrderStatus.Pending,
            CityAdminStaffId = AdminId,
           
        };

        foreach (var item in orderItems)
        {
            OrderItem orderr = new OrderItem
            {
                orderId = order.Id,
                Antigen = item.Antigen,
                Amount = item.Amount,
            };
            await unitOfWork.OrderItemRepo.AddAsync(orderr);
            order.OrderItems.Add(orderr);
        }

        await unitOfWork.OrderRepo.AddAsync(order);
        unitOfWork.SaveAsync();

        return Ok();
    }


    [HttpPost("confirm-order")]
    public async Task<IActionResult> ConfirmOrder(Guid OrderId)
    {

		var AdminId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var Order = await unitOfWork.OrderRepo.FindAsync(e => e.Id == OrderId, false, ["OrderItems"]);
        var Inventory = unitOfWork.MainInventoryRepo.FindByCondition(e => e.CityAdminStaffId == AdminId, false);
          
        foreach (var item in Order.OrderItems)
        {
            var ItemDb=Inventory.FirstOrDefault(e=>e.Antigen == item.Antigen);
			if(ItemDb.Amount<item.Amount)
            {

            }
			
        }

        Order.Status = OrderStatus.Processing;
        Order.DateApproved= DateTime.UtcNow;

        return Ok("تم قبول الطلب");

	}

    [HttpPost("get-order")]
    public async Task<IActionResult> GetOrder(Guid OrderId)
    {
		var AdminId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (AdminId == null)
        {
            return BadRequest("لا يوجد ادمن لهذا المركز");
        }
		var Order = await unitOfWork.OrderRepo.FindAsync(e => e.Id == OrderId, false, ["OrderItems"]);
        if(Order == null)
        {
            return BadRequest("لم يتم العثور على الطلب ");
        }

        var OrganizerId=Order.MedicalStaffId;
        var Organizer = await unitOfWork.MedicalStaffRepo.FindAsync(e => e.UserId == OrganizerId,false);

        OrderDetailsDto OrderDetailsDto = new OrderDetailsDto()
        {
            HealthCareCenterName = Organizer.HealthCareCenterName,
            HealthCareCenterGovernorate = Organizer.HealthCareCenterGovernorate,
            HealthCareCenterCity = Organizer.HealthCareCenterCity,
            HealthCareCenterVillage = Organizer.HealthCareCenterVillage
        };

        foreach(var item in Order.OrderItems)
        {
			OrderDetailsDto.OrderItems.Add(item);

		}

        return Ok(OrderDetailsDto);


	}
    [HttpGet("get-all-oeders-sent")]
    public async Task<IActionResult> GetAllOrdersSent()
    {
		var AdminId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		if (AdminId == null)
		{
			return BadRequest("لا يوجد ادمن لهذا المركز");
		}

        var Orders=  unitOfWork.OrderRepo.FindByCondition(e=>e.CityAdminStaffId == AdminId,false, "OrderItems");

        if (Orders == null)
        {
            return BadRequest("لا يوجد طلبات مرسلة من هذا المركز");
        }

        IList<MyOrderDetailsDto> MyorderDetailsDtos = new List<MyOrderDetailsDto>();

        foreach (var item in Orders) 
        {
            MyOrderDetailsDto orderDetailsDto = new MyOrderDetailsDto()
            {
                Status = item.Status,
                DateApproved = item.DateApproved,
                DateRequested = item.DateRequested,
            };
            foreach(var item2 in item.OrderItems)
            {
				orderDetailsDto.OrderItems.Add(item2);

			}
            MyorderDetailsDtos.Add(orderDetailsDto);

		}
        return Ok(MyorderDetailsDtos);
	}



    [HttpGet]
    public async Task<IActionResult>GetAllOrdersRecived()
    {
		var AdminId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
		if (AdminId == null)
		{
			return BadRequest("لا يوجد ادمن لهذا المركز");
		}

        var Admin = await unitOfWork.MedicalStaffRepo.FindAsync(e => e.UserId == AdminId, false);

        var CityAdmin = Admin.HealthCareCenterCity;

        var Orders = unitOfWork.OrderRepo.FindByCondition(e => e.MedicalStaffId != null && e.MedicalStaff.HealthCareCenterCity==CityAdmin, false);
		var Organizer = await unitOfWork.MedicalStaffRepo.FindAsync(e => e.HealthCareCenterCity==CityAdmin, false);


		List<OrderDetailsDto> orderDetailsDtos = new List<OrderDetailsDto>();
        foreach(var order in  Orders)
        {
            var orderi = new OrderDetailsDto()
            {
                HealthCareCenterName = Organizer.HealthCareCenterCity,
                HealthCareCenterCity = Organizer.HealthCareCenterCity,
                HealthCareCenterGovernorate = Organizer.HealthCareCenterGovernorate,
                HealthCareCenterVillage = Organizer.HealthCareCenterVillage
            };

			foreach (var item in order.OrderItems)
			{
				orderi.OrderItems.Add(item);

			}
            orderDetailsDtos.Add(orderi);
		}
	
        return Ok(orderDetailsDtos);
	}



}
