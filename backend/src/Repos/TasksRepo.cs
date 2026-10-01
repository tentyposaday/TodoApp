using TodoApp.Interfaces;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public class TasksRepo: ITasksRepo
{
    private readonly AppDbContext _context;

    public TasksRepo(AppDbContext dbContext)
    {
        _context = dbContext;
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

    public async Task<List<GroupTaskItem>> GetTasksByGroupId(int groupId)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> AddGroupTask(GroupTaskItem task)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteGroupTask(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<GroupTaskItem> AssignUserToGroupTask(int grouptaskId, int userId)
    {
        throw new NotImplementedException();
    }

}
