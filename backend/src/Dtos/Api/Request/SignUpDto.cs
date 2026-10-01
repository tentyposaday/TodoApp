using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record SignUpDto(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password,
    [Required] bool PasswordConfirmed
);