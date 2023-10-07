namespace Server.API.Models.Dtos;

public class OrdersDto
{
    
    public Shop Shop { get; set; }
    
    public IList<Order> Orders { get; set; } 
    
}