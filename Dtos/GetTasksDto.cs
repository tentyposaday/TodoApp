using System.ComponentModel.DataAnnotations;

namespace Dtos;


public record GetTasksDto(
    int? Id = 0
);