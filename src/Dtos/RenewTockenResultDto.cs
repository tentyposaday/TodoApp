using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record RenewTockenResultDto(
    [Required] bool IsRenewed,
    [Required] string AccessTocken,
    [Required] string RefreshTocken
);