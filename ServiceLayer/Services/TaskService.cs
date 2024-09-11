using DataAccessLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Validation;
using UserTask = DataAccessLayer.Entities.Task;

namespace ServiceLayer.Services;
public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    /// <summary>
    /// method for retrieving user task list
    /// </summary>
    /// <param name="userId">user identifier</param>
    /// <param name="query">query model for filtering sorting and pagination</param>
    /// <returns>list of user tasks</returns>
    /// <exception cref="InvalidCastException">throws when id is not guid</exception>
    public async Task<IEnumerable<UserTask>> GetUserTasksAsync(string userId, QueryModel query)
    {
        var tasks = await _unitOfWork.TaskRepository.GetAllTasksAsync();

        var isIdOk = Guid.TryParse(userId, out Guid userIdGuid);

        IEnumerable<UserTask> userTasks = new List<UserTask>();

        if (isIdOk)
        {
            userTasks = tasks.Where(t => t.UserId == userIdGuid);
        }
        else
        {
            throw new InvalidCastException("invalid id");
        }

        if (query.Priority is not null)
        {
            userTasks = userTasks.Where(t => t.Priority == query.Priority);
        }

        if (query.Status is not null)
        {
            userTasks = userTasks.Where(t => t.Status == query.Status);
        }

        if (query.DueDate is not null)
        {
            userTasks = userTasks.Where(t => t.DueDate == query.DueDate);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("DueDate", StringComparison.OrdinalIgnoreCase))
            {
                userTasks = query.IsDescending ? userTasks.OrderByDescending(t => t.DueDate) : userTasks.OrderBy(t => t.DueDate);
            }

            if (query.SortBy.Equals("Priority", StringComparison.OrdinalIgnoreCase))
            {
                userTasks = query.IsDescending ? userTasks.OrderByDescending(t => t.Priority) : userTasks.OrderBy(t => t.Priority);
            }
        }


        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return userTasks.Skip(skipNumber).Take(query.PageSize);
    }

    public async Task<UserTask> GetUserTaskByIdAsync(string userId, string taskId)
    {
        Guid guidUserId = Guid.Parse(userId);

        var task = await _unitOfWork.TaskRepository.GetByIdAsync(Guid.Parse(taskId));
        if (task.UserId != guidUserId)
        {
            throw new UserMismatchException("user is not correct for provided task");
        }

        if (task is null)
        {
            throw new TaskNotFoundException("task not found");
        }

        return task;
    }



    public async Task AddTaskAsync(TaskModel model, string userId)
    {
        UserTask task = new UserTask();

        task.Title = model.Title;
        task.Description = model.Description;
        task.Status = model.Status;
        task.Priority = model.Priority;
        task.UserId = Guid.Parse(userId);
        task.DueDate = model.DueDate;

        await _unitOfWork.TaskRepository.CreateTaskAsync(task);
        await _unitOfWork.SaveAsync();

    }


    public async Task UpdateUserTaskAsync(TaskModel task, string id)
    {
        var editTask = await _unitOfWork.TaskRepository.GetByIdAsync(Guid.Parse(id));
        editTask.Title = task.Title;
        editTask.DueDate = task.DueDate;
        editTask.Description = task.Description;
        editTask.Status = task.Status;
        editTask.Priority = task.Priority;


        _unitOfWork.TaskRepository.Update(editTask);
        await _unitOfWork.SaveAsync();
    }


    public async Task DeleteUserTask(string taskId)
    {
        bool isValidId = Guid.TryParse(taskId, out Guid id);
        if (isValidId)
        {
            _unitOfWork.TaskRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
