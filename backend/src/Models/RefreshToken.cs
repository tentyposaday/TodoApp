namespace TodoApp.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Value { get; set; }
    public DateTime Expiration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}