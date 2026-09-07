using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Dtos;

public record GetATask(
    [Required] int Id,
    [Required] int UserId
);