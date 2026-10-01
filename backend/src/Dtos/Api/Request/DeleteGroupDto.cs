using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record DeleteGroupDto(
    [Required] int Id
);