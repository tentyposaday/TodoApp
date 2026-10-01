using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.Interfaces;
using TodoApp.Dtos.Request;
using TodoApp.Dtos.Response;
namespace TodoApp.Services;

public class TasksService
{
    private readonly ITasksRepo _tasksRepo;

    public TasksService(ITasksRepo tasksRepo)
    {
        _tasksRepo = tasksRepo;
    }

    public async Task<ViewResultDto> GetTasks(GetTasksDto getTasksDto, int userId)
    {
        if(userId != 0){
        var tasks = await _tasksRepo.GetTasksByUserId(userId);
        return new ViewResultDto(tasks, tasks.Count, "Tasks retrieved successfully.");
        }
        return new ViewResultDto(new List<TaskItem>(), 0, "No tasks found.");
    }

    public async Task<AddResultDto> Add(AddTaskDto addTaskDto, int userId)
    {
        var task = new TaskItem
        {
            Name = addTaskDto.Title,
            Description = addTaskDto.Description,
            UserId = userId
        };

        var isAdded = await _tasksRepo.AddTask(task);
        return new AddResultDto(isAdded, isAdded ? "Task added successfully." : "Failed to add task.");
    }

    public async Task<DeleteResultDto> Delete(int id, int userId)
    {
        var isDeleted = await _tasksRepo.DeleteTask(id, userId);
        return new DeleteResultDto(isDeleted, isDeleted ? "Task deleted successfully." : "Failed to delete task.");
    }
}