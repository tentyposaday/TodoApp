using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface ITasksRepo
{
    IEnumerable<TodoApp.Models.Task> GetTasksByUserId(int userId);
    TodoApp.Models.Task? GetTaskById(int id);
    bool AddTask(TodoApp.Models.Task task);
    bool DeleteTask(int id, int UserId);
}