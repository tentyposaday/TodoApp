using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public record RenewTockenDto(
    [Required] string RefreshTocken
);