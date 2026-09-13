using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Models;
using TodoApp.Interfaces;
using TodoApp.Repositories;
using TodoApp.Dtos;
using System.Security.Claims;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;
    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public IActionResult GetTasks([FromQuery] GetTasksDto getTasksDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tasks = _tasksService.GetTasks(getTasksDto, userId);
        if(tasks.TotalCount == 0)
        {
            return NotFound(tasks);
        }
        return Ok(tasks);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTaskDto addTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var task = await _tasksService.Add(addTaskDto, userId);
        if(task.IsAdded == false)
        {
            return BadRequest(task);
        }
        return CreatedAtAction(
            nameof(GetTasks),
            task
        );    
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteTaskDto deleteTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var result = await _tasksService.Delete(deleteTaskDto.Id, userId);
        if (!result.IsDeleted)
        {
            return NotFound(result);
        }
        return NoContent();
    }
}