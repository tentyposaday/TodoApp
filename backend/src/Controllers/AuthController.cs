using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Models;
using TodoApp.Interfaces;
using TodoApp.Repositories;
using TodoApp.Dtos;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.SignUp(signUpDto);
        if (!result.IsSignedUp)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.Login(loginDto);
        if (!result.Success)
        {
            return Unauthorized(result);
        }
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RenewTockenDto renewTockenDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.GenerateNewAccessToken(renewTockenDto.RefreshTocken);
        if (!result.Success)
        {
            return Unauthorized(result);
        }
        return Ok(result);
    }


    /*[HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDto logoutDto)
    {
        await _authService.Logout(logoutDto);
        return Ok();
    }*/
}