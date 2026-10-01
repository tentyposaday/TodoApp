using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface ITasksRepo
{
    Task<List<TaskItem>> GetTasksByUserId(int userId);
    Task<bool> AddTask(TaskItem task);
    Task<bool> DeleteTask(int id, int UserId);
    Task<List<GroupTaskItem>> GetTasksByGroupId(int groupId);
    Task<bool> AddGroupTask(GroupTaskItem task);
    Task<bool> DeleteGroupTask(int groupTaskId);
    Task<GroupTaskItem> AssignUserToGroupTask(int grouptaskId, int userId);
}