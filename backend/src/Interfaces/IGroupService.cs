using TodoApp.Models;
using TodoApp.Dtos.Request;

namespace TodoApp.Interfaces;

public interface IGroupManagement
{
    Task<Group> CreateGroup(CreateGroupDto createGroupDto, int userId);
    Task<Group?> GetGroupById(int groupId);
    Task<IEnumerable<Group>> GetGroupsByUserId(int userId);
    Task<IEnumerable<Group>> GetAllGroups();
    Task<Group> UpdateGroup(Group group);
    Task<bool> DeleteGroup(int groupId);
    Task<bool> AddUserToGroup(int groupId, int userId);
    Task<bool> RemoveUserFromGroup(int groupId, int userId);
}

// for the signalR GroupHub
public interface IGroupFetching
{
    Task<IEnumerable<Group>> FetchGroupsByUsersId(int userId);
    Task<IEnumerable<int>> FetchUserIdsByGroupIds(int groupId);
}