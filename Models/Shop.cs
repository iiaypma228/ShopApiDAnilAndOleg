using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class Shop
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    [ForeignKey("Director")]
    public int? DirectorId { get; set; }
    
    [NotMapped]
    public Employee? Director { get; set; }
    
    public string? LocationAdress { get; set; }
    
    public string? ShopPhoneNumber { get; set; }

    [NotMapped]
    public List<Employee>? Employees { get; set; }
}