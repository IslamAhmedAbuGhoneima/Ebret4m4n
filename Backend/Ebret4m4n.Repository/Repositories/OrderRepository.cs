using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
