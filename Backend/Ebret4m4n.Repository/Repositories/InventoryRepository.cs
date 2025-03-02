using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Repository.Repositories
{
    public class InventoryRepository :BaseRepository<Inventory>,IInventoryRepositpry
    {
        public InventoryRepository(EbretAmanDbContext context)
            : base(context)
        { }
    }
}
