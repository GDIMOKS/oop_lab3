using Services.Dtos.Product;
using Services.Dtos.Shop;

namespace Services.Interfaces;

public interface IProductService
{
    string AddProduct(string name);
    ShopDto? GetCheapShop(string name);

}