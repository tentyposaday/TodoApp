using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record SignUpDto(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password,
    [Required] bool PasswordConfirmed
);