using Models;
using Interfaces;

namespace Repositories;

public class AccessTokenRepo : IAccessTokenRepo
{
    private List<AccessToken> _accessTokens = new List<AccessToken>();
    public AccessTokenRepo()
    {}

    public async Task<AccessToken?> GetAccessToken(string value)
    {
        var token = _accessTokens.FirstOrDefault(t => t.Value == value);
        return token;
    }

    public async Task<AccessToken> CreateAccessToken(int userId)
    {
        var token = new AccessToken
        {
            Value = Guid.NewGuid().ToString(), // Todo : Implement a proper token generation mechanism
            UserId = userId,
            Expiration = DateTime.UtcNow.AddHours(1) // For now 1 hour expiration, can be changed later
        };
        _accessTokens.Add(token);
        return token;
    }

    public async Task<AccessToken?> DeleteToken(int id)
    {
        var token = _accessTokens.FirstOrDefault(t => t.Id == id);
        if (token != null)
        {
            _accessTokens.Remove(token);
            return token;
        }
        return null;    
        }
}

