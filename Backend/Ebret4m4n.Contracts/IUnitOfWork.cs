namespace Ebret4m4n.Contracts;

public interface IUnitOfWork
{
    IChildRepository ChildRepo { get; }

    IHealthyReportRepository HealthyReportRepo { get; }

    IVaccineRepository VaccineRepo { get; }

    IAppointmentRepository AppointmentRepo { get; }

    IHealthCareCenterRepository HealthCareCenterRepo { get; }

    Task<int> SaveAsync();
}
