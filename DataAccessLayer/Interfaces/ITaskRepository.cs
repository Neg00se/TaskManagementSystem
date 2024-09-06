using UserTask = DataAccessLayer.Entities.Task;

namespace DataAccessLayer.Interfaces;
public interface ITaskRepository
{
    Task<List<UserTask>> GetAllTasksAsync();

    Task<UserTask> GetByIdAsync(Guid id);

    Task CreateTaskAsync(UserTask task);

    void Update(UserTask task);

    void Delete(Guid id);
}
