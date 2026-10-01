using TodoApp.Models;
using TodoApp.Dtos;

namespace TodoApp.Interfaces;

public interface IGroupRepo
{
    Task<Group> CreateGroup(Group group);
    Task<Group?> GetGroupById(int groupId);
    Task<IEnumerable<Group>> GetGroupsByUserId(int userId);
    Task<IEnumerable<Group>> GetAllGroups();
    Task<Group> UpdateGroup(Group group);
    Task<bool> DeleteGroup(int groupId);
    Task<bool> AddUserToGroup(int groupId, int userId);
    Task<bool> RemoveUserFromGroup(int groupId, int userId);
}