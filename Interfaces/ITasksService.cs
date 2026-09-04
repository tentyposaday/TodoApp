using Models;

namespace Interfaces;

public interface ITasksService
{
    IEnumerable<Models.Task> GetAll();
    Models.Task GetById(int id);
    void Add(string title, string description);
}