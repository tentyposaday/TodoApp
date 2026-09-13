using TodoApp.Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.Interfaces;

namespace TodoApp.Repositories;

public class RefreshTokenRepo : IRefreshTokenRepo
{
    private readonly AppDbContext _context;

    public RefreshTokenRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetRefreshToken(string value)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Value == value);
        return token;
    }

    public async Task<bool> CreateRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<RefreshToken?> DeleteRefreshToken(int id)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Id == id);
        if (token != null)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
            return token;
        }
        return null;
    }
}