using TodoApp.Interfaces;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Repositories;

public class UserRepo : IUserRepo
{
    private readonly AppDbContext _context;

    public UserRepo(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        Console.WriteLine($"Searching for user with email: {email}"); // Debugging line
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<bool> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
