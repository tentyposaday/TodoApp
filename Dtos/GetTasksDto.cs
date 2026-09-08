using System.ComponentModel.DataAnnotations;

namespace Dtos;


public record GetTasksDto(
    [Required] int? UserId,
    int? Id = 0
);