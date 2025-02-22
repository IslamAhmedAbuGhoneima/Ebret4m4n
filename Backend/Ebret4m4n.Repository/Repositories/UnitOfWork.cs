using Ebret4m4n.Contracts;

namespace Ebret4m4n.Repository.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly IChildRepository _childRepo;
    private readonly EbretAmanDbContext _dbContext;
    public UnitOfWork(EbretAmanDbContext dbContext)
    {
            _dbContext=dbContext;
        _childRepo = new ChildRepository(_dbContext);
    }

    public IChildRepository ChildRepo => _childRepo;

    public async Task SaveAsync()
       => await _dbContext.SaveChangesAsync();
}
