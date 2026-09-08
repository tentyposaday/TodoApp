using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record LoginResultDto(
    [Required] bool IsAuthorized,
    [Required] string AccessToken,
    [Required] string RefreshToken,
    [Required] int UserId,
    [Required] string Username,
    [Required] string Message
);