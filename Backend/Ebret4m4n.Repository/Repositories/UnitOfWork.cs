using Ebret4m4n.Contracts;

namespace Ebret4m4n.Repository.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly IChildRepository _childRepo;
    private readonly IHealthyReportRepository _healthyReportRepo;
    private readonly IVaccineRepository _vaccineRepo;
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IHealthCareCenterRepository _healthCareCenterRepo;

    private readonly EbretAmanDbContext _dbContext;

    public UnitOfWork(EbretAmanDbContext dbContext)
    {
        _dbContext = dbContext;
        _childRepo = new ChildRepository(_dbContext);
        _healthyReportRepo = new HealthyReportRepository(_dbContext);
        _vaccineRepo = new VaccineRepository(_dbContext);
        _appointmentRepo = new AppointmentRepository(_dbContext);
        _healthCareCenterRepo = new HealthCareCenterRepository(_dbContext);
    }

    public IChildRepository ChildRepo => _childRepo;

    public IHealthyReportRepository HealthyReportRepo => _healthyReportRepo;

    public IVaccineRepository VaccineRepo => _vaccineRepo;

    public IAppointmentRepository AppointmentRepo => _appointmentRepo;

    public IHealthCareCenterRepository HealthCareCenterRepo => _healthCareCenterRepo;

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
