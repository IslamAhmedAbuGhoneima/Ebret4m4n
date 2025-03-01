using Ebret4m4n.Contracts;
using Ebret4m4n.Repository.Repositories;

namespace Ebret4m4n.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IChildRepository _childRepo;
    private readonly IHealthyReportRepository _healthyReportRepo;
    private readonly IVaccineRepository _vaccineRepo;
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IHealthCareCenterRepository _healthCareCenterRepo;
    private readonly IMedicalApplicationRepository _jobApplicationRepo;
    private readonly IMedicalStaffRepository _medicalStaffRepo;
    private readonly IGovernorateAdminStaffRepository _governorateAdminRepo;
    private readonly ICityAdminStaffRepository _cityAdminRepo;

    private readonly EbretAmanDbContext _dbContext;

    public UnitOfWork(EbretAmanDbContext dbContext)
    {
        _dbContext = dbContext;
        _childRepo = new ChildRepository(_dbContext);
        _healthyReportRepo = new HealthyReportRepository(_dbContext);
        _vaccineRepo = new VaccineRepository(_dbContext);
        _appointmentRepo = new AppointmentRepository(_dbContext);
        _healthCareCenterRepo = new HealthCareCenterRepository(_dbContext);
        _jobApplicationRepo = new MedicalApplicationRepository(_dbContext);
        _medicalStaffRepo = new MedicalStaffRepository(_dbContext);
        _governorateAdminRepo = new GovernorateAdminStaffRepository(_dbContext);
        _cityAdminRepo = new CityAdminStaffRepository(_dbContext); 
    }

    public EbretAmanDbContext Context => _dbContext;

    public IChildRepository ChildRepo => _childRepo;

    public IHealthyReportRepository HealthyReportRepo => _healthyReportRepo;

    public IVaccineRepository VaccineRepo => _vaccineRepo;

    public IAppointmentRepository AppointmentRepo => _appointmentRepo;

    public IHealthCareCenterRepository HealthCareCenterRepo => _healthCareCenterRepo;

    public IMedicalApplicationRepository MedicalApplicationRepo => _jobApplicationRepo;

    public IMedicalStaffRepository StaffRepository => _medicalStaffRepo;

    public IGovernorateAdminStaffRepository GovernorateAdminRepo => _governorateAdminRepo;

    public ICityAdminStaffRepository CityAdminRepo => _cityAdminRepo;

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
