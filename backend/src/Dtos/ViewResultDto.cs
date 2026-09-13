using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record ViewResultDto(
    [Required] List<Models.Task> Tasks,
    [Required] int TotalCount,
    [Required] string Message
);