using Ebret4m4n.Contracts;
using Ebret4m4n.Repository.Repositories;

namespace Ebret4m4n.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    readonly IChildRepository _childRepo;
    readonly IHealthyReportRepository _healthyReportRepo;
    readonly IVaccineRepository _vaccineRepo;
    readonly IAppointmentRepository _appointmentRepo;
    readonly IHealthCareCenterRepository _healthCareCenterRepo;
    readonly IMedicalStaffRepository _medicalStaffRepo;
    readonly IGovernorateAdminStaffRepository _governorateAdminRepo;
    readonly ICityAdminStaffRepository _cityAdminRepo;
    readonly IComplaintRepo _complaintRepo;
    readonly IInventoryRepositpry _inventoryRepo;
    readonly IOrderRepository _orderRepo;
    readonly IMainInventoryRepository _mainInventoryRepo;
    readonly IMedicalStaffRepository _mediicalStaffRepo;
    readonly IChatRepository _chatRepo;
    readonly IOrderItemRepository _orderItemRepo;

    readonly EbretAmanDbContext _dbContext;

    public UnitOfWork(EbretAmanDbContext dbContext)
    {
        _dbContext = dbContext;
        _childRepo = new ChildRepository(_dbContext);
        _healthyReportRepo = new HealthyReportRepository(_dbContext);
        _vaccineRepo = new VaccineRepository(_dbContext);
        _appointmentRepo = new AppointmentRepository(_dbContext);
        _healthCareCenterRepo = new HealthCareCenterRepository(_dbContext);
        _medicalStaffRepo = new MedicalStaffRepository(_dbContext);
        _governorateAdminRepo = new GovernorateAdminStaffRepository(_dbContext); 
        _cityAdminRepo = new CityAdminStaffRepository(_dbContext);
        _complaintRepo = new ComplaintRepository(_dbContext);
        _inventoryRepo = new InventoryRepository(_dbContext);
        _orderRepo = new OrderRepository(_dbContext);
        _mainInventoryRepo = new MainInventoryRepository(_dbContext);
        _chatRepo = new ChatRepository(_dbContext);
        _orderItemRepo = new OrderItemRepository(_dbContext);
        _medicalStaffRepo = new MedicalStaffRepository(_dbContext);
    }

    public EbretAmanDbContext Context => _dbContext;

    public IChildRepository ChildRepo => _childRepo;

    public IHealthyReportRepository HealthyReportRepo => _healthyReportRepo;

    public IVaccineRepository VaccineRepo => _vaccineRepo;

    public IAppointmentRepository AppointmentRepo => _appointmentRepo;

    public IHealthCareCenterRepository HealthCareCenterRepo => _healthCareCenterRepo;

    public IMedicalStaffRepository StaffRepo => _medicalStaffRepo;

    public IGovernorateAdminStaffRepository GovernorateAdminRepo => _governorateAdminRepo;

    public IComplaintRepo ComplaintRepo => _complaintRepo;

    public ICityAdminStaffRepository CityAdminStaffRepo => _cityAdminRepo;

    public IInventoryRepositpry InventoryRepo => _inventoryRepo;

    public IOrderRepository OrderRepo => _orderRepo;
    public IOrderItemRepository OrderItemRepo => _orderItemRepo;

    public IMainInventoryRepository MainInventoryRepo => _mainInventoryRepo;

    public IMedicalStaffRepository mMdicalStaffRepository => _medicalStaffRepo;
    public IChatRepository ChatRepo => _chatRepo;

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
