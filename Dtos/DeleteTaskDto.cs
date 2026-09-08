using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record DeleteTaskDto(
    [Required] int Id,
    [Required] int UserId
);