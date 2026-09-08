using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record AddTaskDto(
    [Required] string Title, 
    [Required] string Description,
    [Required] int UserId
);