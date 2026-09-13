using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record DeleteTaskDto(
    [Required] int Id
);