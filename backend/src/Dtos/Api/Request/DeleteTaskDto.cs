using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record DeleteTaskDto(
    [Required] int Id
);