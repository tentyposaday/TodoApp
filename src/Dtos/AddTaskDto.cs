using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record AddTaskDto(
    [Required] string Title, 
    [Required] string Description
);