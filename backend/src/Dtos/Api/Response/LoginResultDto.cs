using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Response;

public record LoginResultDto(
    [Required] bool Success,
    [Required] string AccessToken,
    [Required] int ExpirationTimeInSeconds,
    [Required] string RefreshToken,
    [Required] string Username,
    [Required] string Message
);