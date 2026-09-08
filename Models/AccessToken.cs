namespace Models;


public class AccessToken
{
    public int Id { get; set; }
    public string Value { get; set; }
    public DateTime Expiration { get; set; }
    public int UserId { get; set; }
}