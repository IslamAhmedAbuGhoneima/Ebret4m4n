using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class CityAdminStaffRepository : BaseRepository<CityAdminStaff>, ICityAdminStaffRepository
{
    public CityAdminStaffRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
