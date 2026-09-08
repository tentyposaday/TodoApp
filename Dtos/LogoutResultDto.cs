using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record LogoutResultDto(
    [Required] bool Success,
    [Required] string Message
);