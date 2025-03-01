using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class MedicalApplicationRepository : BaseRepository<MedicalApplication>, IMedicalApplicationRepository
{
    public MedicalApplicationRepository(EbretAmanDbContext context)
        : base(context)
    { }
}
