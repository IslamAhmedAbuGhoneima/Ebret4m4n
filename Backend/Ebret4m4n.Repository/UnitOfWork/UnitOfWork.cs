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
    private readonly IJobApplicationRepository _jobApplicationRepo;
    private readonly IMedicalStaffRepository _medicalStaffRepo;

    private readonly EbretAmanDbContext _dbContext;

    public UnitOfWork(EbretAmanDbContext dbContext)
    {
        _dbContext = dbContext;
        _childRepo = new ChildRepository(_dbContext);
        _healthyReportRepo = new HealthyReportRepository(_dbContext);
        _vaccineRepo = new VaccineRepository(_dbContext);
        _appointmentRepo = new AppointmentRepository(_dbContext);
        _healthCareCenterRepo = new HealthCareCenterRepository(_dbContext);
        _jobApplicationRepo = new JobApplicationRepository(_dbContext);
        _medicalStaffRepo = new MedicalStaffRepository(_dbContext);
    }

    public EbretAmanDbContext Context => _dbContext;

    public IChildRepository ChildRepo => _childRepo;

    public IHealthyReportRepository HealthyReportRepo => _healthyReportRepo;

    public IVaccineRepository VaccineRepo => _vaccineRepo;

    public IAppointmentRepository AppointmentRepo => _appointmentRepo;

    public IHealthCareCenterRepository HealthCareCenterRepo => _healthCareCenterRepo;

    public IJobApplicationRepository JobApplicationRepo => _jobApplicationRepo;

    public IMedicalStaffRepository StaffRepository => _medicalStaffRepo;

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
