using Models;
using Dtos;

namespace Interfaces;

public interface IAuthService
{
    Task<SignUpResultDto> SignUp(SignUpDto signUpDto);
    Task<LoginResultDto> Login(LoginDto loginDto);
    // Task<LogoutResultDto> Logout(LogoutDto logoutDto);
    // Task<RenewTockenResultDto> RenewTocken(RenewTockenDto renewTockenDto);
    string GenerateJwtToken(User user);
    Task<string?> GenerateRefreshToken(int userId);
    Task<LoginResultDto> GenerateNewAccessToken(string refreshToken);
}