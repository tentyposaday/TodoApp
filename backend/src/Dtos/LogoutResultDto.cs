using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record LogoutResultDto(
    [Required] bool Success,
    [Required] string Message
);