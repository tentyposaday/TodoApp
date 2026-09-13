using TodoApp.Models;

namespace TodoApp.Interfaces;

public interface IRefreshTokenRepo
{
    Task<RefreshToken?> GetRefreshToken(string value);
    Task<bool> CreateRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> DeleteRefreshToken(int id);
}