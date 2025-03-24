using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Repository.Repositories
{
	public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(EbretAmanDbContext context) : base(context)
		{
		}
	}
}
