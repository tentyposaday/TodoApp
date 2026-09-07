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
    public IActionResult GetTasks([FromQuery] GetTasks getTasksDto)
    {
        var tasks = _tasksService.GetTasks(getTasksDto);
        return Ok(tasks);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] AddTask addTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var task = _tasksService.Add(addTaskDto);
        return CreatedAtAction(
            nameof(GetTasks),
            task
        );    
    }

    [HttpPost("delete")]
    public IActionResult Delete([FromBody] DeleteTask deleteTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _tasksService.Delete(deleteTaskDto.Id, deleteTaskDto.UserId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}