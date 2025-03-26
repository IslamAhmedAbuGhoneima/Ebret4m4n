using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
	public OrderItemRepository(EbretAmanDbContext context)
		: base(context)
	{ }
}
