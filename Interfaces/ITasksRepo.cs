using Models;

namespace Interfaces;

public interface ITasksRepo
{
    IEnumerable<Models.Task> GetAll();
    Models.Task GetById(int id);
    void Add(Models.Task task);
}