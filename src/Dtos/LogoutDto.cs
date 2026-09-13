using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record LogoutDto(
    [Required] string RefreshToken,
    [Required] string AccessToken,
    [Required] int UserId
);