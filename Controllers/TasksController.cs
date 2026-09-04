using Microsoft.AspNetCore.Mvc;
using Models;
using Interfaces;
using Repositories;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(ITasksService tasksService)
    {
        return Ok(tasksService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(ITasksService tasksService, int id)
    {
        var task = tasksService.GetById(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }
    
    [HttpPost]
    public IActionResult Add(ITasksService tasksService, [FromBody] Dtos.AddTask addTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        tasksService.Add(addTaskDto.Title, addTaskDto.Description);
        return CreatedAtAction(nameof(GetById), new { id = tasksService.GetAll().Last().Id }, tasksService.GetAll().Last());
    }
}