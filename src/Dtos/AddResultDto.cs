using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record AddResultDto(
    [Required] bool IsAdded,
    [Required] string Message
);