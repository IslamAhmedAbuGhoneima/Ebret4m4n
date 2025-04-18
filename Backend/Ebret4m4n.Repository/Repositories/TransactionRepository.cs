using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;


namespace Ebret4m4n.Repository.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(EbretAmanDbContext context)
        : base(context)
    { }
}
