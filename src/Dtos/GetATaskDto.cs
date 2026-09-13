using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Dtos;

public record GetATaskDto(
    [Required] int Id
);