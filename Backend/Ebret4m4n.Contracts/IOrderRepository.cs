using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Contracts;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order?> GetOrderWithStaffAsync(Guid orderId);
}
