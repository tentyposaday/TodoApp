using Microsoft.AspNetCore.Mvc;
using Models;
using Interfaces;
using Repositories;
using Dtos;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var tasks = _tasksService.GetTasks(getTasksDto);
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
        var task = await _tasksService.Add(addTaskDto);
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
        var result = await _tasksService.Delete(deleteTaskDto.Id, deleteTaskDto.UserId);
        if (!result.IsDeleted)
        {
            return NotFound(result);
        }
        return NoContent();
    }
}