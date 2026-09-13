using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface ITasksRepo
{
    Task<List<TaskItem>> GetTasksByUserId(int userId);
    Task<TaskItem?> GetTaskById(int id);
    Task<bool> AddTask(TaskItem task);
    Task<bool> DeleteTask(int id, int UserId);
}