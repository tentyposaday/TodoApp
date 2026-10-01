using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Response;

public record LogoutResultDto(
    [Required] bool Success,
    [Required] string Message
);