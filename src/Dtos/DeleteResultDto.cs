using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record DeleteResultDto(
    [Required] bool IsDeleted,
    [Required] string Message
);