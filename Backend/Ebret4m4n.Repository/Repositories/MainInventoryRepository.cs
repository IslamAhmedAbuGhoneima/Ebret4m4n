using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class MainInventoryRepository : BaseRepository<MainInventory>, IMainInventoryRepository
{
    public MainInventoryRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
