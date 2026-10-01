namespace TodoApp.Models;

public class Group
{
    public int Id { get; set; }
    public List<int> UserIds { get; set; }
    public int OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}