using Models;
using Repositories;
using Interfaces;
using Dtos;

namespace Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepo _tasksRepo;

    public TasksService(ITasksRepo tasksRepo)
    {
        _tasksRepo = tasksRepo;
    }

    public ViewResultDto GetTasks(GetTasksDto getTasksDto)
    {
        if(getTasksDto.UserId != null)
        {
            var tasks = _tasksRepo.GetAll().Where(t => t.UserId == getTasksDto.UserId).ToList();
            return new ViewResultDto(tasks, tasks.Count, "Tasks retrieved successfully.");
        }

        return new ViewResultDto(new List<Models.Task>(), 0, "No tasks found.");
    }

    public async Task<AddResultDto> Add(AddTaskDto addTaskDto)
    {
        var task = new Models.Task
        {
            Name = addTaskDto.Title,
            Description = addTaskDto.Description,
            UserId = addTaskDto.UserId
        };

        var isAdded = _tasksRepo.Add(task);
        return new AddResultDto(isAdded, isAdded ? "Task added successfully." : "Failed to add task.");
    }

    public async Task<DeleteResultDto> Delete(int id, int userId)
    {
        var isDeleted = _tasksRepo.Delete(id, userId);
        return new DeleteResultDto(isDeleted, isDeleted ? "Task deleted successfully." : "Failed to delete task.");
    }
}