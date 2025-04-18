using Ebret4m4n.Contracts;
using Ebret4m4n.Repository.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

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
    readonly IChatRepository _chatRepo;
    readonly IOrderItemRepository _orderItemRepo;
    readonly INotificationRepository _notificationRepo;
    readonly ITransactionRepository _transactionRepo;

    readonly EbretAmanDbContext _dbContext;
    private IDbContextTransaction? _transaction;


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
        _notificationRepo = new NotificationRepository(_dbContext);
        _transactionRepo = new TransactionRepository(_dbContext);
    }

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

    public IChatRepository ChatRepo => _chatRepo;

    public INotificationRepository NotificationRepo => _notificationRepo;

    public ITransactionRepository TransactionRepo => _transactionRepo;

    public async Task BeginTransactionAsync()
        => _transaction = await _dbContext.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        if(_transaction is not null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }

    public async Task<int> SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
