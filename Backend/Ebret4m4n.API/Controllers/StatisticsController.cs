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
		string? gov = User.FindFirst("governorate")?.Value;
		if (gov is null) return Unauthorized("Governorate claim missing");
		return Ok(await GetStatistics(gov));
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
		var childrenQuery = _unitOfWork.ChildRepo.FindAll(false, "User", "Vaccines");
		var complaintsQuery = _unitOfWork.ComplaintRepo.FindAll(false, "User");
		var vaccinesQuery = _unitOfWork.VaccineRepo.FindAll(false, "Child.User");
		var healthUnitsQuery = _unitOfWork.HealthCareCenterRepo.FindAll(false);

		if (governorate is not null)
		{
			childrenQuery = childrenQuery.Where(c => c.User.Governorate == governorate);
			complaintsQuery = complaintsQuery.Where(c => c.User.Governorate == governorate);
			vaccinesQuery = vaccinesQuery.Where(v => v.Child.User.Governorate == governorate);
			healthUnitsQuery = healthUnitsQuery.Where(h => h.Governorate == governorate);
		}

		if (city is not null)
		{
			childrenQuery = childrenQuery.Where(c => c.User.City == city);
			complaintsQuery = complaintsQuery.Where(c => c.User.City == city);
			vaccinesQuery = vaccinesQuery.Where(v => v.Child.User.City == city);
			healthUnitsQuery = healthUnitsQuery.Where(h => h.City == city);
		}

		var children = await childrenQuery.ToListAsync();
		var complaints = await complaintsQuery.ToListAsync();
		var vaccines = await vaccinesQuery.ToListAsync();
		var healthUnits = await healthUnitsQuery.ToListAsync();

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

		var unitReport = vaccines
			.Where(v => v.IsTaken && v.Child.User.HealthCareCenter != null)
			.GroupBy(v => v.Child.User.HealthCareCenter.HealthCareCenterName)
			.Select(g => new UnitReportDto
			{
				UnitName = g.Key,
				ComplaintsCount = complaints.Count(c => c.User.HealthCareCenter != null && c.User.HealthCareCenter.HealthCareCenterName == g.Key),
				VaccinesTaken = g.Count()
			}).ToList();

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

		var cityReport = vaccines
			.Where(v => v.IsTaken)
			.GroupBy(v => v.Child.User.City)
			.Select(g => new CityReportDto
			{
				City = g.Key ?? "",
				HealthUnitCount = healthUnits.Count(h => h.City == g.Key),
				VaccinesTaken = g.Count()
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


