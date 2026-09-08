using System.ComponentModel.DataAnnotations;

namespace Dtos;

public record RenewTockenResultDto(
    [Required] bool IsRenewed,
    [Required] string AccessTocken,
    [Required] string RefreshTocken
);