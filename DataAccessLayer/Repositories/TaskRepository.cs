using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserTask = DataAccessLayer.Entities.Task;

namespace DataAccessLayer.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly TaskSystemDbContext _context;

    public TaskRepository(TaskSystemDbContext context)
    {
        _context = context;
    }

    public async Task CreateTaskAsync(UserTask task)
    {
        if (task is not null)
        {
            await _context.Tasks.AddAsync(task);

        }
    }

    public void Delete(Guid id)
    {
        var task = _context.Tasks.Find(id);
        if (task is not null)
        {
            _context.Tasks.Remove(task);
        }
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return tasks;
    }

    public async Task<UserTask> GetByIdAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return task;
    }

    public void Update(UserTask task)
    {
        _context.Tasks.Update(task);
    }
}
