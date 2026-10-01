using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;


public record GetTasksDto(
    int? Id = 0
);