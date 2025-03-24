namespace Ebret4m4n.Contracts;

public interface IUnitOfWork
{
    IChildRepository ChildRepo { get; }

    IHealthyReportRepository HealthyReportRepo { get; }

    IVaccineRepository VaccineRepo { get; }

    IAppointmentRepository AppointmentRepo { get; }

    IHealthCareCenterRepository HealthCareCenterRepo { get; }

    IMedicalStaffRepository StaffRepo { get; }

    IComplaintRepo ComplaintRepo { get; }

    IGovernorateAdminStaffRepository GovernorateAdminRepo { get; }

    ICityAdminStaffRepository CityAdminStaffRepo { get; }

    IInventoryRepositpry InventoryRepo { get; }

    IOrderRepository OrderRepo { get; }
    IOrderItemRepository OrderItemRepo { get; }

    IMainInventoryRepository MainInventoryRepo { get; }
    IMedicalStaffRepository MedicalStaffRepo { get; }

    IChatRepository ChatRepo { get; }

    Task<int> SaveAsync();
}
