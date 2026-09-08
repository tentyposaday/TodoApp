using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record AddResultDto(
    [Required] bool IsAdded,
    [Required] string Message
);