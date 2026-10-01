using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos.Request;

public record RenewTockenDto(
    [Required] string RefreshTocken
);