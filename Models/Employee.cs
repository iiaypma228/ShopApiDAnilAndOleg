using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class Employee
{
    public int? Id { get; set; }
    
    
    [ForeignKey("Shop")]
    public int? ShopId { get; set; } //номер магазина на котором работает сотрудник, -5 будет значить что сотрудник имеет доступ ко всем магазинам.
    
    [NotMapped]
    public Shop? Shop { get; set; }
    
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public string? Phone { get; set; }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }
}