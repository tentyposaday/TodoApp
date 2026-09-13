using Models;
using Dtos;
namespace Interfaces;

public interface ITasksService
{
    ViewResultDto GetTasks(GetTasksDto getTasksDto, int userId);
    Task<AddResultDto> Add(AddTaskDto addTaskDto, int userId);
    Task<DeleteResultDto> Delete(int id, int userId);
}