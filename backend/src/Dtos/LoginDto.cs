using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record LoginDto(
    [Required] string Email,
    [Required] string Password
);