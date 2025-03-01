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
<<<<<<< HEAD
    private readonly IGovernorateAdminStaffRepository _governorateAdminRepo;
    private readonly ICityAdminStaffRepository _cityAdminRepo;
=======
    private readonly IComplaintRepo _complaintRepo;
>>>>>>> aa38714b6d67c28c4a36b1b428bf68773177ce26

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
<<<<<<< HEAD
        _governorateAdminRepo = new GovernorateAdminStaffRepository(_dbContext);
        _cityAdminRepo = new CityAdminStaffRepository(_dbContext); 
=======
        _complaintRepo = new ComplaintRepository(_dbContext);
>>>>>>> aa38714b6d67c28c4a36b1b428bf68773177ce26
    }

    public EbretAmanDbContext Context => _dbContext;

    public IChildRepository ChildRepo => _childRepo;

    public IHealthyReportRepository HealthyReportRepo => _healthyReportRepo;

    public IVaccineRepository VaccineRepo => _vaccineRepo;

    public IAppointmentRepository AppointmentRepo => _appointmentRepo;

    public IHealthCareCenterRepository HealthCareCenterRepo => _healthCareCenterRepo;

    public IMedicalApplicationRepository MedicalApplicationRepo => _jobApplicationRepo;

    public IMedicalStaffRepository StaffRepository => _medicalStaffRepo;

<<<<<<< HEAD
    public IGovernorateAdminStaffRepository GovernorateAdminRepo => _governorateAdminRepo;

    public ICityAdminStaffRepository CityAdminRepo => _cityAdminRepo;
=======
    public IComplaintRepo complaintRepo => _complaintRepo;
>>>>>>> aa38714b6d67c28c4a36b1b428bf68773177ce26

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
