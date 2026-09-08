using Models;
using Repositories;
using Interfaces;
using Dtos;
using BCrypt.Net;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IRefreshTokenRepo _refreshTokenRepo;
    private readonly IAccessTokenRepo _accessTokenRepo;

    public AuthService(IUserRepo userRepo, IRefreshTokenRepo refreshTokenRepo, IAccessTokenRepo accessTokenRepo)
    {
        _userRepo = userRepo;
        _refreshTokenRepo = refreshTokenRepo;
        _accessTokenRepo = accessTokenRepo;
    }

    public async Task<SignUpResultDto> SignUp(SignUpDto signUpDto)
    {
        if(!signUpDto.PasswordConfirmed)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Passwords do not match",
                AccessToken: "",
                RefreshToken: "",
                UserId: 0,
                Username: ""
            );
        }
        if(_userRepo.GetUserByEmail(signUpDto.Email) != null)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Email already exists",
                AccessToken: "",
                RefreshToken: "",
                UserId: 0,
                Username: ""
            );
        }
        if(_userRepo.GetUserByUsername(signUpDto.Username) != null)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Username already exists",
                AccessToken: "",
                RefreshToken: "",
                UserId: 0,
                Username: ""
            );
        }

        var user = new User
        {
            Email = signUpDto.Email,
            Username = signUpDto.Username,
            EncryptedPassword = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password),
            CreatedAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userRepo.AddUser(user);

        if(result)
        {
            var refreshToken = await _refreshTokenRepo.CreateRefreshToken(user.Id);
            var accessToken = await _accessTokenRepo.CreateAccessToken(user.Id);
            return new SignUpResultDto(
                IsSignedUp: true,
                Message: "User created successfully",
                AccessToken: accessToken.Value,
                RefreshToken: refreshToken.Value,
                UserId: user.Id,
                Username: user.Username
            );
        }
        else
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "User creation failed",
                AccessToken: "",
                RefreshToken: "",
                UserId: 0,
                Username: ""
            );
        }

    }

    public async Task<LoginResultDto> IsAuthorized(LoginDto loginDto)
    {
        var user = await _userRepo.GetUserByEmail(loginDto.Email);
        if(user == null)
        {
            return new LoginResultDto(
                IsAuthorized: false,
                Message: "User not found",
                AccessToken: "",
                RefreshToken: "",
                UserId: 0,
                Username: ""
            );
        }
        if(BCrypt.Net.BCrypt.Verify(loginDto.Password, user.EncryptedPassword))
        {
            var refreshToken = await _refreshTokenRepo.CreateRefreshToken(user.Id);
            var accessToken = await _accessTokenRepo.CreateAccessToken(user.Id);
            return new LoginResultDto(
                IsAuthorized: true,
                Message: "User authorized",
                AccessToken: accessToken.Value,
                RefreshToken: refreshToken.Value,
                UserId: user.Id,
                Username: user.Username
            );
        }

        return new LoginResultDto(
            IsAuthorized: false,
            Message: "Invalid credentials",
            AccessToken: "",
            RefreshToken: "",
            UserId: 0,
            Username: ""
        );
    }

    public async Task<LogoutResultDto> Logout(LogoutDto logoutDto)
    {
        var refreshToken = await _refreshTokenRepo.GetRefreshToken(logoutDto.RefreshToken);
        if(refreshToken == null)
        {
            return new LogoutResultDto(
                Success: false,
                Message: "Invalid refresh token"
            );
        }
        var accessToken = await _accessTokenRepo.GetAccessToken(logoutDto.AccessToken);
        if(accessToken != null)
        {
            await _accessTokenRepo.DeleteToken(accessToken.Id);
        }
        else
        {
            return new LogoutResultDto(
                Success: false,
                Message: "Invalid access token"
            );
        }
        await _refreshTokenRepo.DeleteRefreshToken(refreshToken.Id);
        return new LogoutResultDto(
            Success: true,
            Message: "User logged out successfully"
        );
    }
}