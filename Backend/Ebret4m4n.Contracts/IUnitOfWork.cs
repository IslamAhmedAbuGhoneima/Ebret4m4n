using Ebret4m4n.Contracts;

namespace Ebret4m4n.Contracts;

public interface IUnitOfWork
{
    IChildRepository ChildRepo { get; }

    IHealthyReportRepository HealthyReportRepo { get; }

    IVaccineRepository VaccineRepo { get; }

    IAppointmentRepository AppointmentRepo { get; }

    IHealthCareCenterRepository HealthCareCenterRepo { get; }

    IJobApplicationRepository JobApplicationRepo { get; }

    IMedicalStaffRepository StaffRepository { get; }

    Task<int> SaveAsync();
}
