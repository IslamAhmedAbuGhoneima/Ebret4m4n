using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class HealthyReportRepository : BaseRepository<HealthReportFile>, IHealthyReportRepository
{
    public HealthyReportRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
