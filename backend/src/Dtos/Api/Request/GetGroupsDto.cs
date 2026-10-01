using System.ComponentModel.DataAnnotations;
namespace TodoApp.Dtos.Request;

public record GetGroupsDto(
    [Required] int UserId
);