using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface IUserRepo
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByUsername(string username);
    Task<bool> AddUser(User user);
    Task<bool> UpdateUser(User user);
}