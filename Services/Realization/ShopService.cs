using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Dtos.Product;
using Services.Dtos.Shop;
using Services.Interfaces;

namespace Services.Realization;

public class ShopService : IShopService
{
    private ShopDbContext _dbContext;

    public ShopService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public int AddShop(int code, string name, string address)
    {
        var shop = new Shop {Code = code, Name = name, Address = address}; 
        var newShop = _dbContext.Shops.Add(shop);
        _dbContext.SaveChanges();

        return newShop.Entity.Code;
    }

    public void AddProductsSupply(int shopCode, List<ProvideProductsDto> dtos)
    {
        var shop = _dbContext.Shops.FirstOrDefault(x => x.Code == shopCode);
        if (shop == null)
            return;
        
        var shopProducts = dtos.Select(x => new ShopProduct
        {
            ShopCode = shopCode,
            ProductName = x.ProductName,
            Price = x.Price,
            Quantity = x.Quantity
        }).ToList().Distinct();

        var existProducts = _dbContext.ShopProducts
            .Where(x => x.ShopCode == shopCode)
            .Where(x => dtos.Select(y => y.ProductName).Contains(x.ProductName));

        var newProducts = shopProducts.ExceptBy(existProducts.Select(x => x.ProductName), x => x.ProductName);
        _dbContext.ShopProducts.AddRange(newProducts);

        foreach (var existProduct in existProducts)
        {
            existProduct.Quantity += shopProducts.FirstOrDefault(y => y.ProductName == existProduct.ProductName).Quantity;
            existProduct.Price = shopProducts.FirstOrDefault(y => y.ProductName == existProduct.ProductName).Price;
        }
        
        _dbContext.SaveChanges();
    }

    public List<ProvideProductsDto> CheckMROT(int shopCode, decimal cash)
    {
        var products = _dbContext.ShopProducts
            .Where(x => x.ShopCode == shopCode)
            .Where(x => x.Price <= cash && x.Price > 0)
            .Where(x => x.Quantity > 0)
            .Include(x => x.Product);

        var availableProducts = new List<ProvideProductsDto>();
        foreach (var product in products)
        {
            int count = Decimal.ToInt32(cash / product.Price);
            if (product.Quantity < count)
                count = product.Quantity;
            
            availableProducts.Add(new ProvideProductsDto() {ProductName = product.ProductName, Price = product.Price, Quantity = count});
            
        }

        return availableProducts;
    }

    public decimal MakePurchase(int shopCode, List<PurchaseProductDto> dtos)
    {
        var products = _dbContext.ShopProducts
            .Where(x => x.ShopCode == shopCode)
            .Where(x => dtos.Select(y => y.ProductName).Contains(x.ProductName));
        
        decimal sum = 0;
        foreach (var product in products)
        {
            var neededQuantity = dtos.FirstOrDefault(x => x.ProductName == product.ProductName).Quantity;
            if (product.Quantity >= neededQuantity)
            {
                sum += product.Price * neededQuantity;
                product.Quantity -= neededQuantity;
            }
            else
            {
                sum = -1;
                break;
            }
        }
        
        if (sum != -1)
            _dbContext.SaveChanges();
        return sum;
    }

    public ShopSumDto? CheckMinSum(List<PurchaseProductDto> dtos)
    {
        /*var shops = _dbContext.ShopProducts
            .GroupBy(x => x.ShopCode)
            .Where(x => dtos.IntersectBy(x.));

        var neededShops = new List<ShopSumDto>();
        foreach (var shop in shops)
        {
            foreach (var shopProduct in shop)
            {
                
            }
        }*/
        
        return new ShopSumDto();
    }


}