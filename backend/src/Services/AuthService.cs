using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.Interfaces;
using TodoApp.Dtos;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace TodoApp.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepo _refreshTokenRepo;

    public AuthService(IUserRepo userRepo, IConfiguration configuration, IRefreshTokenRepo refreshTokenRepo)
    {
        _userRepo = userRepo;
        _configuration = configuration;
        _refreshTokenRepo = refreshTokenRepo;
    }

    public async Task<SignUpResultDto> SignUp(SignUpDto signUpDto)
    {
        if(!signUpDto.PasswordConfirmed)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Passwords do not match",
                Username: ""
            );
        }
        var existingUserByEmail = await _userRepo.GetUserByEmail(signUpDto.Email);
        if(existingUserByEmail != null)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Email already exists",
                Username: ""
            );
        }
        var existingUserByUsername = await _userRepo.GetUserByUsername(signUpDto.Username);
        if(existingUserByUsername != null)
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "Username already exists",
                Username: ""
            );
        }

        var user = new User
        {
            Email = signUpDto.Email,
            Username = signUpDto.Username,
            EncryptedPassword = Hash(signUpDto.Password),
            CreatedAt = DateTime.UtcNow,
            LastLogin = null,
            IsActive = true
        };

        var result = await _userRepo.AddUser(user);

        if(result)
        {
            return new SignUpResultDto(
                IsSignedUp: true,
                Message: "User created successfully",
                Username: user.Username
            );
        }
        else
        {
            return new SignUpResultDto(
                IsSignedUp: false,
                Message: "User creation failed",
                Username: ""
            );
        }

    }

    public async Task<LoginResultDto> Login(LoginDto loginDto)
    {
        var user = await _userRepo.GetUserByEmail(loginDto.Email);
        if(user == null)
        {
            return new LoginResultDto(
                Success: false,
                Message: "User not found",
                AccessToken: "",
                RefreshToken: "",
                ExpirationTimeInSeconds: 0,
                Username: ""
            );
        }
        if (!user.IsActive)
        {
            return new LoginResultDto(Success: false, Message: "Account is inactive",AccessToken:"", RefreshToken:"", ExpirationTimeInSeconds:0, Username:"");
        }
        if(BCrypt.Net.BCrypt.Verify(loginDto.Password, user.EncryptedPassword))
        {
            var accessToken = GenerateJwtToken(user);
            var refreshToken = await GenerateRefreshToken(user.Id);
            if(refreshToken == null || accessToken == null)
            {
                // Log "Token generation Failed!!"
                return new LoginResultDto(
                    Success: false,
                    Message: "Something went wrong. Please try again.",
                    AccessToken: "",
                    RefreshToken: "",
                    ExpirationTimeInSeconds: 0,
                    Username: ""
                );
            }
            user.LastLogin = DateTime.UtcNow;
            await _userRepo.UpdateUser(user);
            return new LoginResultDto(
                Success: true,
                Message: "User authenticated successfuly",
                AccessToken: accessToken,
                ExpirationTimeInSeconds: _configuration.GetValue<int>("JwtConfig:AccessTokenExpiration") * 60,
                RefreshToken: refreshToken,
                Username: user.Username
            );
        }

        return new LoginResultDto(
            Success: false,
            Message: "Invalid credentials",
            AccessToken: "",
            RefreshToken: "",
            ExpirationTimeInSeconds: 0,
            Username: ""
        );
    }

/*    public async Task<LogoutResultDto> Logout(LogoutDto logoutDto)
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
    } */

    public string GenerateJwtToken(User user)
    {
        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]!);
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:AccessTokenExpiration");
        var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: tokenExpiration,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return accessToken;
    }

    public async Task<string?> GenerateRefreshToken(int userId)
    {
        var refreshTokenValidityMins = _configuration.GetValue<int>("JwtConfig:RefreshTokenExpiration");
        var rawValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Value = HashToken(rawValue),
            Expiration = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins)
        };
        var result = await _refreshTokenRepo.CreateRefreshToken(refreshToken);
        if (!result)
        {
            return null;
        }
        return rawValue;
    }

    public async Task<LoginResultDto> GenerateNewAccessToken(string refreshToken)
    {
        var hashedToken = HashToken(refreshToken);
        var token = await _refreshTokenRepo.GetRefreshToken(hashedToken);
        if(token == null)
        {
            return new LoginResultDto(
                Success: false,
                AccessToken: "", 
                ExpirationTimeInSeconds: 0, 
                RefreshToken: "", 
                Username: "", 
                Message: "Unvalid Token");
        }
        else if(token.Expiration < DateTime.UtcNow)
        {
            await _refreshTokenRepo.DeleteRefreshToken(token.Id);
            return new LoginResultDto(
                Success: false, 
                AccessToken: "", 
                ExpirationTimeInSeconds: 0, 
                RefreshToken: "", 
                Username: "", 
                Message: "Expired Token");
        }
        var user = await _userRepo.GetUserById(token.UserId);
        if(user == null)
        {
            await _refreshTokenRepo.DeleteRefreshToken(token.Id);
            return new LoginResultDto(false, "", 0, "", "", "User not found");
        }
        var newToken = GenerateJwtToken(user);

        if(newToken == null)
        {
            return new LoginResultDto(
                Success: false, 
                AccessToken: "", 
                ExpirationTimeInSeconds: 0, 
                RefreshToken: "", 
                Username: "", 
                Message: "Server Error");
        }
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:AccessTokenExpiration");
        var tokenExpiration = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        
        var newRefreshToken = await GenerateRefreshToken(user.Id);
        if(newRefreshToken == null)
        {
            newRefreshToken = refreshToken;
        }
        else
        {
            await _refreshTokenRepo.DeleteRefreshToken(token.Id); // invalidate old token
        }
        return new LoginResultDto(
            Success: true, 
            AccessToken: newToken,
            ExpirationTimeInSeconds: tokenValidityMins*60  ,
            RefreshToken: newRefreshToken,
            Username: user.Username,
            Message: "Ok");
    }

    private string Hash(string value)
    {
        return BCrypt.Net.BCrypt.HashPassword(value);
    }
    private string HashToken(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes);
    }
}