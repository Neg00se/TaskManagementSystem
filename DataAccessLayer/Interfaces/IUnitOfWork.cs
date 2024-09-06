namespace DataAccessLayer.Interfaces;
public interface IUnitOfWork
{
    ITaskRepository TaskRepository { get; }
    IUserRepository UserRepository { get; }

    Task SaveAsync();
}