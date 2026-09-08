using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Interfaces;

namespace Repositories;

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

    public async Task<RefreshToken> CreateRefreshToken(int userId)
    {
        var token = new RefreshToken
        {
            Value = Guid.NewGuid().ToString(), // Todo : Implement a proper token generation mechanism
            UserId = userId,
            Expiration = DateTime.UtcNow.AddDays(7) // For now 7 days expiration, can be changed later
        };
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
        return token;
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