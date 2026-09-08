using Models;

namespace Interfaces;

public interface IRefreshTokenRepo
{
    Task<RefreshToken?> GetRefreshToken(string value);
    Task<RefreshToken> CreateRefreshToken(int userId);
    Task<RefreshToken?> DeleteRefreshToken(int id);
}