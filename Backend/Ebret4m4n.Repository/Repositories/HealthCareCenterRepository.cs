using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class HealthCareCenterRepository : BaseRepository<HealthCareCenter>, IHealthCareCenterRepository
{
    public HealthCareCenterRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
