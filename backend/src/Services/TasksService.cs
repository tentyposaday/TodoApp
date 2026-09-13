using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.Interfaces;
using TodoApp.Dtos;

namespace TodoApp.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepo _tasksRepo;

    public TasksService(ITasksRepo tasksRepo)
    {
        _tasksRepo = tasksRepo;
    }

    public ViewResultDto GetTasks(GetTasksDto getTasksDto, int userId)
    {
        if(userId != null)
        {
            var tasks = _tasksRepo.GetTasksByUserId(userId).ToList();
            return new ViewResultDto(tasks, tasks.Count, "Tasks retrieved successfully.");
        }

        return new ViewResultDto(new List<Models.Task>(), 0, "No tasks found.");
    }

    public async Task<AddResultDto> Add(AddTaskDto addTaskDto, int userId)
    {
        var task = new Models.Task
        {
            Name = addTaskDto.Title,
            Description = addTaskDto.Description,
            UserId = userId
        };

        var isAdded = _tasksRepo.AddTask(task);
        return new AddResultDto(isAdded, isAdded ? "Task added successfully." : "Failed to add task.");
    }

    public async Task<DeleteResultDto> Delete(int id, int userId)
    {
        var isDeleted = _tasksRepo.DeleteTask(id, userId);
        return new DeleteResultDto(isDeleted, isDeleted ? "Task deleted successfully." : "Failed to delete task.");
    }
}