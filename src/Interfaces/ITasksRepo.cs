using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface ITasksRepo
{
    IEnumerable<Models.Task> GetAll();
    Models.Task? GetById(int id);
    bool Add(Models.Task task);
    bool Delete(int id, int UserId);
}