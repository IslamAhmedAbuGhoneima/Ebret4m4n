using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs.StatisticsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController(IUnitOfWork _unitOfWork) : ControllerBase
{
    private async Task<(int maleCount, int femaleCount, int fullyVaccinated, double vaccinesTaken)> GetBaseStats(
        ICollection<Child> children, ICollection<Vaccine> vaccines)
    {
        var maleCount = children.Count(c => c.Gender == 'M');
        var femaleCount = children.Count(c => c.Gender == 'F');
        var fullyVaccinated = children.Count(c => c.Vaccines != null && c.Vaccines.All(v => v.IsTaken));
        var vaccinesTaken = vaccines.Count(v => v.IsTaken) / 1_000_000.0;

        return (maleCount, femaleCount, fullyVaccinated, vaccinesTaken);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<AdminDto>> GetAdminStats()
    {
        try
        {
            var children = await _unitOfWork.ChildRepo
                .FindAll(false, "Vaccines", "User", "User.HealthCareCenter")
                .ToListAsync();

            var vaccines = await _unitOfWork.VaccineRepo
                .FindAll(false, "Child.User", "Child.User.HealthCareCenter")
                .ToListAsync();

            var complaints = await _unitOfWork.ComplaintRepo
                .FindAll(false, "User", "User.HealthCareCenter")
                .CountAsync();

            var healthUnits = await _unitOfWork.HealthCareCenterRepo
                .FindAll(false)
                .CountAsync();

            var orders = await _unitOfWork.OrderItemRepo
                .FindAll(false, "Order", "Order.GovernorateAdminStaff")
                .ToListAsync();

            var (maleCount, femaleCount, fullyVaccinated, vaccinesTaken) = await GetBaseStats(children, vaccines);

            var topGovs = orders
                .Where(o => o.Order?.GovernorateAdminStaff != null)
                .GroupBy(o => o.Order.GovernorateAdminStaff!.Governorate)
                .Select(g => new GovernorateVaccineRequestDto
                {
                    Governorate = g.Key,
                    TotalVaccinesRequested = g.Sum(x => (int)x.Amount)
                })
                .OrderByDescending(x => x.TotalVaccinesRequested)
                .Take(10)
                .ToList();

            var govReports = new List<GovernorateReportDto>();
            var govAdmins = await _unitOfWork.GovernorateAdminRepo
                .FindAll(false, "CityAdminStaffs")
                .ToListAsync();

            foreach (var gov in govAdmins)
            {
                var healthUnitCount = await _unitOfWork.HealthCareCenterRepo
                    .FindByCondition(h => h.Governorate == gov.Governorate, false)
                    .CountAsync();

                var vaccineCount = await _unitOfWork.VaccineRepo
                    .FindByCondition(v => v.IsTaken && v.Child.User.Governorate == gov.Governorate, false, "Child.User")
                    .CountAsync();

                govReports.Add(new GovernorateReportDto
                {
                    Governorate = gov.Governorate,
                    CityCount = gov.CityAdminStaffs.Select(c => c.City).Distinct().Count(),
                    HealthUnitCount = healthUnitCount,
                    VaccinesTaken = vaccineCount
                });
            }

            var vaccineRequests = orders
                .GroupBy(o => o.Antigen)
                .Select(g => new VaccineRequestDto
                {
                    VaccineName = g.Key,
                    RequestedAmount = g.Sum(x => (int)x.Amount)
                })
                .OrderByDescending(x => x.RequestedAmount)
                .ToList();

            return Ok(new AdminDto
            {
                HealthCareUnits = healthUnits,
                RegisteredChildren = children.Count,
                FullyVaccinatedChildren = fullyVaccinated,
                TotalVaccinesTaken = vaccinesTaken,
                TotalComplaints = complaints,
                MaleChildren = maleCount,
                FemaleChildren = femaleCount,
                TopGovernoratesByVaccines = topGovs,
                GovernoratesReport = govReports,
                VaccineRequests = vaccineRequests
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }

    [HttpGet("governorate")]
    [Authorize(Roles = "governorateAdmin")]
    public async Task<ActionResult<StatisticssDto>> GetGovernorateStats()
    {
        try
        {
            var governorate = User.FindFirst("governorate")?.Value;
            if (governorate == null) return Unauthorized("Governorate claim missing");

            var children = await _unitOfWork.ChildRepo
                .FindByCondition(c => c.User.Governorate == governorate, false, "Vaccines", "User", "User.HealthCareCenter")
                .ToListAsync();

            var vaccines = await _unitOfWork.VaccineRepo
                .FindByCondition(v => v.Child.User.Governorate == governorate, false, "Child.User", "Child.User.HealthCareCenter")
                .ToListAsync();

            var complaints = await _unitOfWork.ComplaintRepo
                .FindByCondition(c => c.User.Governorate == governorate, false)
                .CountAsync();

            var healthUnits = await _unitOfWork.HealthCareCenterRepo
                .FindByCondition(h => h.Governorate == governorate, false)
                .ToListAsync();

            var (maleCount, femaleCount, fullyVaccinated, vaccinesTaken) = await GetBaseStats(children, vaccines);

            var cityAdmins = await _unitOfWork.CityAdminStaffRepo
                .FindByCondition(c => c.Governorate == governorate && c.City != null, false)
                .ToListAsync();

            var cityStats = new List<CityVaccineRequestDto>();
            var cityReports = new List<CityReportDto>();

            foreach (var cityAdmin in cityAdmins)
            {
                var cityVaccineCount = vaccines.Count(v => v.IsTaken && v.Child.User.City == cityAdmin.City);
                
                cityStats.Add(new CityVaccineRequestDto
                {
                    City = cityAdmin.City!,
                    RequestedAmount = cityVaccineCount
                });

                var cityHealthUnitCount = healthUnits.Count(h => h.City == cityAdmin.City);

                cityReports.Add(new CityReportDto
                {
                    City = cityAdmin.City!,
                    HealthUnitCount = cityHealthUnitCount,
                    VaccinesTaken = cityVaccineCount
                });
            }

            cityStats = cityStats
                .OrderByDescending(x => x.RequestedAmount)
                .Take(15)
                .ToList();

            return Ok(new StatisticssDto
            {
                TotalComplaints = complaints,
                TotalVaccinesTaken = vaccinesTaken,
                FullyVaccinatedChildren = fullyVaccinated,
                RegisteredChildren = children.Count,
                HealthCareUnits = healthUnits.Count,
                MaleChildren = maleCount,
                FemaleChildren = femaleCount,
                TopHealthCareUnitsByVaccines = new(),
                AllUnits = new(),
                TopCitiesByVaccines = cityStats,
                AllCities = cityReports
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }

    [HttpGet("city")]
    [Authorize(Roles = "cityAdmin")]
    public async Task<ActionResult<CityStatsDto>> GetCityStats()
    {
        try
        {
            var governorate = User.FindFirst("governorate")?.Value;
            var city = User.FindFirst("city")?.Value;
            if (governorate == null || city == null) 
                return Unauthorized("Governorate or City claim missing");

            var children = await _unitOfWork.ChildRepo
                .FindByCondition(c => c.User.City == city, false, "Vaccines", "User", "User.HealthCareCenter")
                .ToListAsync();

            var vaccines = await _unitOfWork.VaccineRepo
                .FindByCondition(v => v.Child.User.City == city, false, "Child.User", "Child.User.HealthCareCenter")
                .ToListAsync();

            var complaints = await _unitOfWork.ComplaintRepo
                .FindByCondition(c => c.User.City == city, false)
                .CountAsync();

            var healthUnits = await _unitOfWork.HealthCareCenterRepo
                .FindByCondition(h => h.City == city, false)
                .ToListAsync();

            var (maleCount, femaleCount, fullyVaccinated, vaccinesTaken) = await GetBaseStats(children, vaccines);

            var unitStats = healthUnits
                .Select(unit => new UnitVaccineRequestDto
                {
                    UnitName = unit.HealthCareCenterName,
                    RequestedAmount = vaccines.Count(v => 
                        v.IsTaken && 
                        v.Child.User.HealthCareCenter != null && 
                        v.Child.User.HealthCareCenter.HealthCareCenterName == unit.HealthCareCenterName)
                })
                .Where(x => x.RequestedAmount > 0)
                .OrderByDescending(x => x.RequestedAmount)
                .Take(15)
                .ToList();

            var unitReports = healthUnits
                .Select(unit => new UnitReportDto
                {
                    UnitName = unit.HealthCareCenterName,
                    ComplaintsCount = _unitOfWork.ComplaintRepo
                        .FindByCondition(c => 
                            c.User.HealthCareCenter != null && 
                            c.User.HealthCareCenter.HealthCareCenterName == unit.HealthCareCenterName, 
                            false)
                        .Count(),
                    VaccinesTaken = vaccines.Count(v => 
                        v.IsTaken && 
                        v.Child.User.HealthCareCenter != null && 
                        v.Child.User.HealthCareCenter.HealthCareCenterName == unit.HealthCareCenterName)
                })
                .ToList();

            return Ok(new CityStatsDto
            {
                TotalComplaints = complaints,
                TotalVaccinesTaken = vaccinesTaken,
                FullyVaccinatedChildren = fullyVaccinated,
                RegisteredChildren = children.Count,
                HealthCareUnits = healthUnits.Count,
                MaleChildren = maleCount,
                FemaleChildren = femaleCount,
                TopHealthCareUnitsByVaccines = unitStats,
                AllUnits = unitReports,
                TopVaccinesTaken = new(),
                AllCities = new()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }
}


