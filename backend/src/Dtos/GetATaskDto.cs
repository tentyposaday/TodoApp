using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace TodoApp.Dtos;

public record GetATaskDto(
    [Required] int Id
);