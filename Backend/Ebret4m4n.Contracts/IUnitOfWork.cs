namespace Ebret4m4n.Contracts;

public interface IUnitOfWork
{
    IChildRepository ChildRepo { get; }


    Task SaveAsync();
}
