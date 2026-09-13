using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record SignUpResultDto(
    [Required] bool IsSignedUp,
    [Required] string Message,
    [Required] string Username
);