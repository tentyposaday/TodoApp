using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;


public record GetTasksDto(
    int? Id = 0
);