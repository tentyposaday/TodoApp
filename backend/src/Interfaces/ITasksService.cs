using TodoApp.Models;
using TodoApp.Dtos.Request;
using TodoApp.Dtos.Response;
namespace TodoApp.Interfaces;

public interface ITasksService
{
    Task<ViewResultDto> GetTasks(GetTasksDto getTasksDto, int userId);
    Task<AddResultDto> Add(AddTaskDto addTaskDto, int userId);
    Task<DeleteResultDto> Delete(int id, int userId);
}