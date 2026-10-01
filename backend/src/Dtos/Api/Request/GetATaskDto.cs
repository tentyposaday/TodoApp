using System.ComponentModel.DataAnnotations;
namespace TodoApp.Dtos.Request;

public record GetATaskDto(
    [Required] int Id
);