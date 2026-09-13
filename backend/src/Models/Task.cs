namespace TodoApp.Models;

public class Task
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}