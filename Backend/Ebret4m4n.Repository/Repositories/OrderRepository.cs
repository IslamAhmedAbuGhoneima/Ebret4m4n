using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Ebret4m4n.Repository.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly EbretAmanDbContext _context;

    public OrderRepository(EbretAmanDbContext context) 
        : base(context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderWithStaffAsync(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.CityAdminStaff)
            .Include(o => o.GovernorateAdminStaff)
            .Include(o => o.MedicalStaff)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

}
