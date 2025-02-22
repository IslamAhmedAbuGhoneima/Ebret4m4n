using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class VaccineRepository : BaseRepository<Vaccine>, IVaccineRepository
{
    public VaccineRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
