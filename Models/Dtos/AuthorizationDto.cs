namespace Server.API.Models.Dtos;

public class AuthorizationDto
{
    public string Email { get; set; }
    public Employee Employee { get; set; }
    
    public Shop Shop { get; set; }
    
    public string Token { get; set; }
    
    public int TokenLifeTime { get; set; }
}