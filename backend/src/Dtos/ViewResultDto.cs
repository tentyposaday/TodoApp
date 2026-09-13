using System.ComponentModel.DataAnnotations;
using TodoApp.Models;

namespace TodoApp.Dtos;

public record ViewResultDto(
    [Required] List<TaskItem> Tasks,
    [Required] int TotalCount,
    [Required] string Message
);