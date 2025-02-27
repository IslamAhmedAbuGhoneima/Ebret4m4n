using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class JobApplicationRepository : BaseRepository<JobApplications>, IJobApplicationRepository
{
    public JobApplicationRepository(EbretAmanDbContext context)
        : base(context)
    { }
}
