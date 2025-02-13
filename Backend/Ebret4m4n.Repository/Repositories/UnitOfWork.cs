using Ebret4m4n.Contracts;

namespace Ebret4m4n.Repository.Repositories;

public class UnitOfWork(EbretAmanDbContext context) : IUnitOfWork
{
    public async Task SaveAsync()
       => await context.SaveChangesAsync();
}
