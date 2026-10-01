using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record CreateGroupDto(
    [Required] string Name,
    [Required] string Description
);