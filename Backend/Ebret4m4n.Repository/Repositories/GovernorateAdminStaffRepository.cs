using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class GovernorateAdminStaffRepository : BaseRepository<GovernorateAdminStaff>, IGovernorateAdminStaffRepository
{
    public GovernorateAdminStaffRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
