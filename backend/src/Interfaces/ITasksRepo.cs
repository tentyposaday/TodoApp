using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface ITasksRepo
{
    Task<List<TodoApp.Models.Task>> GetTasksByUserId(int userId);
    Task<TodoApp.Models.Task?> GetTaskById(int id);
    Task<bool> AddTask(TodoApp.Models.Task task);
    Task<bool> DeleteTask(int id, int UserId);
}