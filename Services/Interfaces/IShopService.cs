using Services.Dtos.Product;
using Services.Dtos.Shop;

namespace Services.Interfaces;

public interface IShopService
{
    int AddShop(int code, string name, string address);
    void AddProductsSupply(int shopCode, List<ProvideProductsDto> dtos);

}