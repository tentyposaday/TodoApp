using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Response;

public record DeleteResultDto(
    [Required] bool IsDeleted,
    [Required] string Message
);