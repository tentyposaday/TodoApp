using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Models;
using TodoApp.Interfaces;
using TodoApp.Repositories;
using TodoApp.Dtos.Request;
using System.Security.Claims;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupController : ControllerBase
{
    private readonly IGroupManagement _groupmanager;
    public GroupController(IGroupManagement groupService)
    {
        _groupmanager = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] GetGroupsDto getGroupsDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToGroup([FromBody] CreateGroupDto createGroupDto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        throw new NotImplementedException();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int GroupId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        throw new NotImplementedException();
    }
}