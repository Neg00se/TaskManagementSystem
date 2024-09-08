using DataAccessLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Validation;
using UserTask = DataAccessLayer.Entities.Task;

namespace ServiceLayer.Services;
public class TaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<List<UserTask>> GetUserTasksAsync(Guid userId)
    {
        var tasks = await _unitOfWork.TaskRepository.GetAllTasksAsync();
        var userTasks = tasks.Where(t => t.UserId == userId).ToList();
        if (userTasks is null)
        {
            throw new TaskNotFoundException("task not found");
        }
        return userTasks;
    }

    public async Task<UserTask> GetUserTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
        if (task.UserId != userId)
        {
            throw new UserMismatchException("user is not correct for provided task");
        }

        if (task is null)
        {
            throw new TaskNotFoundException("task not found");
        }

        return task;
    }

    public async Task<List<UserTask>> SortAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserTask>> GetByFilterAsync()
    {
        throw new NotImplementedException();

    }

    public async Task AddTaskAsync(TaskModel model, string userId)
    {
        UserTask task = new UserTask();

        task.Title = model.Title;
        task.Description = model.Description;
        task.Status = model.Status;
        task.Priority = model.Priority;
        task.UserId = model.UserId;
        task.DueDate = model.DueDate;

        await _unitOfWork.TaskRepository.CreateTaskAsync(task);
        await _unitOfWork.SaveAsync();

    }


    public async Task UpdateUserTaskAsync(UserTask task)
    {
        _unitOfWork.TaskRepository.Update(task);
        await _unitOfWork.SaveAsync();
    }


    public async Task DeleteUserTask(Guid taskId)
    {
        _unitOfWork.TaskRepository.Delete(taskId);
        await _unitOfWork.SaveAsync();
    }
}
