using System.ComponentModel.DataAnnotations;

namespace Dtos;


public record GetTasks(
    [Required] int? UserId,
    int? Id = 0
);