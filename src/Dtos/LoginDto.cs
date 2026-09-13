using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record LoginDto(
    [Required] string Email,
    [Required] string Password
);