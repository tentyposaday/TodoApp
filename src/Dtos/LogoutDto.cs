using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record LogoutDto(
    [Required] string RefreshToken,
    [Required] string AccessToken,
    [Required] int UserId
);