using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Response;

public record SignUpResultDto(
    [Required] bool IsSignedUp,
    [Required] string Message,
    [Required] string Username
);