using ServiceLayer.Models;

namespace ServiceLayer.Interfaces;
public interface ITaskService
{
    Task AddTaskAsync(TaskModel model, string userId);
    Task DeleteUserTask(string taskId);
    Task<DataAccessLayer.Entities.Task> GetUserTaskByIdAsync(string userId, string taskId);
    Task<IEnumerable<DataAccessLayer.Entities.Task>> GetUserTasksAsync(string userId, QueryModel query);
    Task UpdateUserTaskAsync(TaskModel task, string id);
}