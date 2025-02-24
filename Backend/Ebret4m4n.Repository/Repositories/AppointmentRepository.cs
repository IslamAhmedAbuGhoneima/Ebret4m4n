using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

internal class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
