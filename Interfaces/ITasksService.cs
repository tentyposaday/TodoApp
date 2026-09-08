using Models;
using Dtos;
namespace Interfaces;

public interface ITasksService
{
    ViewResultDto GetTasks(GetTasksDto getTasksDto);
    Task<AddResultDto> Add(AddTaskDto addTaskDto);
    Task<DeleteResultDto> Delete(int id, int userId);
}