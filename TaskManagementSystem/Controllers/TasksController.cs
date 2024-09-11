using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Validation;
using System.Security.Claims;
using UserTask = DataAccessLayer.Entities.Task;

namespace TaskManagementSystem.Controllers;
[Route("/[controller]")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult> Add(TaskModel model)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _taskService.AddTaskAsync(model, userId);
            return Ok();
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserTask>>> Get([FromQuery] QueryModel filter)
    {
        try
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var taskList = await _taskService.GetUserTasksAsync(userId, filter);
            return Ok(taskList);

        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserTask>> GetById(string id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await _taskService.GetUserTaskByIdAsync(userId, id);

            return Ok(task);

        }
        catch (UserMismatchException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(TaskModel task, string id)
    {
        try
        {
            await _taskService.UpdateUserTaskAsync(task, id);
            return Ok();
        }
        catch (Exception ex)
        {

            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _taskService.DeleteUserTask(id);
            return Ok();
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }
}
