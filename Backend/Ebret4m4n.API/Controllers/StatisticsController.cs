using Ebret4m4n.Contracts;
using Ebret4m4n.Shared.DTOs.StatisticsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController(IUnitOfWork _unitOfWork) : ControllerBase
{
    [HttpGet("admin")]
	[Authorize(Roles = "admin")]
	public async Task<ActionResult<AdminDto>> GetAdminStats()
	{
		var children = await _unitOfWork.ChildRepo.FindAll(false, "Vaccines", "User").ToListAsync();
		var vaccines = await _unitOfWork.VaccineRepo.FindAll(false).ToListAsync();
		var complaints = await _unitOfWork.ComplaintRepo.FindAll(false).CountAsync();
		var healthUnits = await _unitOfWork.HealthCareCenterRepo.FindAll(false).CountAsync();
		var orders = await _unitOfWork.OrderItemRepo.FindAll(false, "Order").ToListAsync();
		var allGovs = await _unitOfWork.GovernorateAdminRepo.FindAll(false, "CityAdminStaffs").ToListAsync();

		int fullyVaccinated = children.Count(c => c.Vaccines != null && c.Vaccines.All(v => v.IsTaken));
		int male = children.Count(c => c.Gender == 'M');
		int female = children.Count(c => c.Gender == 'F');
		double vaccinesTaken = vaccines.Count(v => v.IsTaken) / 1_000_000.0;

		var topGovs = orders
			.Where(o => o.Order.GovernorateAdminStaff != null)
			.GroupBy(o => o.Order.GovernorateAdminStaff.Governorate)
			.Select(g => new GovernorateVaccineRequestDto
			{
				Governorate = g.Key,
				TotalVaccinesRequested = g.Sum(x => (int)x.Amount)
			})
			.OrderByDescending(g => g.TotalVaccinesRequested)
			.Take(10)
			.ToList();

		var vaccineRequests = orders
			.GroupBy(o => o.Antigen)
			.Select(g => new VaccineRequestDto
			{
				VaccineName = g.Key,
				RequestedAmount = g.Sum(x => (int)x.Amount)
			})
			.ToList();

		var report = new List<GovernorateReportDto>();
		foreach (var gov in allGovs)
		{
			var govName = gov.Governorate;
			var cities = gov.CityAdminStaffs.Select(c => c.City).Distinct().ToList();
			var healthUnitCount = await _unitOfWork.HealthCareCenterRepo
				.FindByCondition(h => h.Governorate == govName, false)
				.CountAsync();

			var takenVaccines = await _unitOfWork.VaccineRepo
				.FindByCondition(v => v.IsTaken && v.Child.User.Governorate == govName, false, "Child.User")
				.CountAsync();

			report.Add(new GovernorateReportDto
			{
				Governorate = govName,
				CityCount = cities.Count,
				HealthUnitCount = healthUnitCount,
				VaccinesTaken = takenVaccines
			});
		}

		return Ok(new AdminDto
		{
			HealthCareUnits = healthUnits,
			RegisteredChildren = children.Count,
			FullyVaccinatedChildren = fullyVaccinated,
			TotalVaccinesTaken = vaccinesTaken,
			TotalComplaints = complaints,
			MaleChildren = male,
			FemaleChildren = female,
			TopGovernoratesByVaccines = topGovs,
			GovernoratesReport = report,
			VaccineRequests = vaccineRequests
		});
	}

	[HttpGet("governorate")]
	[Authorize(Roles = "governorateAdmin")]
	public async Task<ActionResult<StatisticssDto>> GetGovernorateStats()
	{
		string? governorate = User.FindFirst("governorate")?.Value;
		if (governorate is null) return Unauthorized("Governorate claim missing");

		return Ok(await GetStatistics(governorate));

	}


	[HttpGet("city")]
	[Authorize(Roles = "cityAdmin")]
	public async Task<ActionResult<StatisticssDto>> GetCityStats()
	{
		string? gov = User.FindFirst("governorate")?.Value;
		string? city = User.FindFirst("city")?.Value;
		if (gov is null || city is null) return Unauthorized("Governorate or City claim missing");
		return Ok(await GetStatistics(gov, city));
	}

	private async Task<StatisticssDto> GetStatistics(string? governorate = null, string? city = null)
	{
		var childrenQuery = await _unitOfWork.ChildRepo
			.FindAll(false, "User", "Vaccines")
			.ToListAsync();

		var complaintsQuery = await _unitOfWork.ComplaintRepo
			.FindAll(false, "User")
			.ToListAsync();

		var vaccinesQuery = await _unitOfWork.VaccineRepo
			.FindAll(false, "Child.User")
			.ToListAsync();

		var healthUnitsQuery = await _unitOfWork.HealthCareCenterRepo
			.FindAll(false)
			.ToListAsync();

		var cityNames = await _unitOfWork.CityAdminStaffRepo.FindAll(false)
			.Where(u => u.Governorate == governorate && u.City != null)
			.Select(u => u.City!.Trim())
			.Distinct()
			.ToListAsync();

		if (governorate is not null)
		{
			childrenQuery = childrenQuery.Where(c => c.User.Governorate == governorate).ToList();
			complaintsQuery = complaintsQuery.Where(c => c.User.Governorate == governorate).ToList();
			vaccinesQuery = vaccinesQuery.Where(v => v.Child.User.Governorate == governorate).ToList();
			healthUnitsQuery = healthUnitsQuery.Where(h => h.Governorate == governorate).ToList();
		}

		if (city is not null)
		{
			childrenQuery = childrenQuery.Where(c => c.User.City == city).ToList();
			complaintsQuery = complaintsQuery.Where(c => c.User.City == city).ToList();
			vaccinesQuery = vaccinesQuery.Where(v => v.Child.User.City == city).ToList();
			healthUnitsQuery = healthUnitsQuery.Where(h => h.City == city).ToList();
		}

		var children = childrenQuery;
		var complaints = complaintsQuery;
		var vaccines = vaccinesQuery;
		var healthUnits = healthUnitsQuery;

		int fullyVaccinated = children.Count(c => c.Vaccines != null && c.Vaccines.All(v => v.IsTaken));
		int registeredChildren = children.Count;
		int male = children.Count(c => c.Gender == 'M');
		int female = children.Count(c => c.Gender == 'F');
		double totalVaccinesTaken = vaccines.Count(v => v.IsTaken) / 1_000_000.0;

		var topUnits = vaccines
			.Where(v => v.IsTaken && v.Child.User.HealthCareCenter != null)
			.GroupBy(v => v.Child.User.HealthCareCenter.HealthCareCenterName)
			.Select(g => new UnitVaccineRequestDto
			{
				UnitName = g.Key,
				RequestedAmount = g.Count()
			})
			.OrderByDescending(x => x.RequestedAmount)
			.Take(15)
			.ToList();

		var unitReport = await _unitOfWork.HealthCareCenterRepo.FindByCondition
			(e=>e.City==city,false)
			.Select(g => new UnitReportDto
			{
				UnitName = g.HealthCareCenterName,
				ComplaintsCount = _unitOfWork.ComplaintRepo.FindAll(false)
			.Count(c => c.User.HealthCareCenter != null &&
						c.User.HealthCareCenter.HealthCareCenterName == g.HealthCareCenterName),

				VaccinesTaken = _unitOfWork.VaccineRepo.FindAll(false)
			.Count(v => v.IsTaken &&
						v.Child.User.HealthCareCenter != null &&
						v.Child.User.HealthCareCenter.HealthCareCenterName == g.HealthCareCenterName)

			}).ToListAsync();

		var topCities = vaccines
			.Where(v => v.IsTaken)
			.GroupBy(v => v.Child.User.City)
			.Select(g => new CityVaccineRequestDto
			{
				City = g.Key ?? "",
				RequestedAmount = g.Count()
			})
			.OrderByDescending(x => x.RequestedAmount)
			.Take(15)
			.ToList();

		var cityReport = cityNames
	.Select(cityName => new CityReportDto
	{
		City = cityName!,
		HealthUnitCount = healthUnits.Count(h => h.City?.Trim() == cityName),
		VaccinesTaken = vaccines.Count(v => v.IsTaken && v.Child.User.City?.Trim() == cityName)
	}).ToList();

		return new StatisticssDto
		{
			TotalComplaints = complaints.Count,
			TotalVaccinesTaken = totalVaccinesTaken,
			FullyVaccinatedChildren = fullyVaccinated,
			RegisteredChildren = registeredChildren,
			MaleChildren = male,
			FemaleChildren = female,
			HealthCareUnits = healthUnits.Count,
			TopHealthCareUnitsByVaccines = city != null ? topUnits : new(),
			AllUnits = city != null ? unitReport : new(),
			TopCitiesByVaccines = city == null ? topCities : new(),
			AllCities = city == null ? cityReport : new()
		};
	}
}


