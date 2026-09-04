using Interfaces;
using Models;

namespace Repositories;

public class TasksRepo: ITasksRepo
{
    private readonly List<Models.Task> _tasks;
    private int _nextId = 1;

    public TasksRepo()
    {
        _tasks = new List<Models.Task>();
    }

    public IEnumerable<Models.Task> GetAll()
    {
        return _tasks;
    }

    public Models.Task GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public void Add(Models.Task task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
    }
}