using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record RenewTockenDto(
    [Required] string RefreshTocken
);