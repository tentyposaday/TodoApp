using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Response;

public record AddResultDto(
    [Required] bool IsAdded,
    [Required] string Message
);