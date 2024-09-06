using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly TaskSystemDbContext _context;

    public UnitOfWork(TaskSystemDbContext context)
    {
        _context = context;
    }


    public IUserRepository UserRepository => new UserRepository(_context);

    public ITaskRepository TaskRepository => new TaskRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

}
