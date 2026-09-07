using Models;
using Dtos;
namespace Interfaces;

public interface ITasksService
{
    List<Models.Task> GetTasks(GetTasks getTasksDto);
    Task<Models.Task> Add(AddTask addTaskDto);
    bool Delete(int id, int userId);
    
}