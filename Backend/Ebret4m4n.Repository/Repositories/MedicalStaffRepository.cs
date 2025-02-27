using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class MedicalStaffRepository : BaseRepository<MedicalStaff>, IMedicalStaffRepository
{
    public MedicalStaffRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
