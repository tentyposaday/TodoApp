using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record DeleteResultDto(
    [Required] bool IsDeleted,
    [Required] string Message
);