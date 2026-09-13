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
    public async Task<IActionResult> GetTasks([FromQuery] GetTasksDto getTasksDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tasks =  await _tasksService.GetTasks(getTasksDto, userId);
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
            return BadRequest(task.Message);
        }
        return StatusCode(StatusCodes.Status201Created, task);  
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var result = await _tasksService.Delete(id, userId);
        if (!result.IsDeleted)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}