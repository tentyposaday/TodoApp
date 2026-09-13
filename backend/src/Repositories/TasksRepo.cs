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

    public async Task<List<TodoApp.Models.Task>> GetTasksByUserId(int userId)
    {
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Models.Task?> GetTaskById(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> AddTask(Models.Task task)
    {
        try
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            // Log failure here
            return false;
        }
    }

    public async Task<bool> DeleteTask(int id, int UserId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == UserId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
