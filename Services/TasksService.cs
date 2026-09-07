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

    public List<Models.Task> GetTasks(GetTasks getTasksDto)
    {
        if(getTasksDto.UserId != null)
        {
            return _tasksRepo.GetAll().Where(t => t.UserId == getTasksDto.UserId).ToList();
        }

        return new List<Models.Task>();
    }

    public async Task<Models.Task> Add(AddTask addTaskDto)
    {
        var task = new Models.Task
        {
            Name = addTaskDto.Title,
            Description = addTaskDto.Description,
            UserId = addTaskDto.UserId
        };

        _tasksRepo.Add(task);
        return task;
    }

    public bool Delete(int id, int userId)
    {
        return _tasksRepo.Delete(id, userId);
    }
}