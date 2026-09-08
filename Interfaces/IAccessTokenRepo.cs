using Models;

namespace Interfaces;

public interface IAccessTokenRepo
{
    Task<AccessToken?> GetAccessToken(string value);
    Task<AccessToken> CreateAccessToken(int userId);
    Task<AccessToken?> DeleteToken(int id);
}