using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record LoginDto(
    [Required] string Email,
    [Required] string Password
);