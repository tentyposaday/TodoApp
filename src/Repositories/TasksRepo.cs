using TodoApp.Interfaces;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public class TasksRepo: ITasksRepo
{
    private readonly List<Models.Task> _tasks;
    private int _nextId = 1;
    private readonly AppDbContext _context;

    public TasksRepo(AppDbContext dbContext)
    {
        _context = dbContext;
        _tasks = new List<Models.Task>();
    }

    public IEnumerable<Models.Task> GetAll()
    {
        return _context.Tasks.ToList();
    }

    public Models.Task? GetById(int id)
    {
        return _context.Tasks.FirstOrDefault(t => t.Id == id);
    }

    public bool Add(Models.Task task)
    {
        try
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(int id, int UserId)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == UserId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
