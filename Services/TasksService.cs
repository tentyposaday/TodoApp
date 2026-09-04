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

    public IEnumerable<Models.Task> GetAll()
    {
        return _tasksRepo.GetAll();
    }

    public Models.Task GetById(int id)
    {
        return _tasksRepo.GetById(id);
    }

    public void Add(string title, string description)
    {
        var task = new Models.Task
        {
            Name = title,
            Description = description
        };

        _tasksRepo.Add(task);
    }
}