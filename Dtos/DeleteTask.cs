using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record DeleteTask(
    [Required] int Id,
    [Required] int UserId
);