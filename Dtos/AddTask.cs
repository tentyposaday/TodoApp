using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record AddTask(
    [Required] string Title, 
    [Required] string Description
);