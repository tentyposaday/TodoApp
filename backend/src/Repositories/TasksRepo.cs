using TodoApp.Interfaces;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public class TasksRepo: ITasksRepo
{
    private readonly List<TaskItem> _tasks;
    private int _nextId = 1;
    private readonly AppDbContext _context;

    public TasksRepo(AppDbContext dbContext)
    {
        _context = dbContext;
        _tasks = new List<TaskItem>();
    }

    public async Task<List<TaskItem>> GetTasksByUserId(int userId)
    {
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<bool> AddTask(TaskItem task)
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
