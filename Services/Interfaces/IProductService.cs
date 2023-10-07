using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface IProductService  : ICRUDService<Product>
{
    CategoryProduct ReadCategoryProduct(int id);
}