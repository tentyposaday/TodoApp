using Models;
using Dtos;

namespace Interfaces;

public interface IAuthService
{
    Task<SignUpResultDto> SignUp(SignUpDto signUpDto);
    Task<LoginResultDto> IsAuthorized(LoginDto loginDto);
    Task<LogoutResultDto> Logout(LogoutDto logoutDto);
    // Task<RenewTockenResultDto> RenewTocken(RenewTockenDto renewTockenDto);
}