using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Dtos.Product;
using Services.Dtos.Shop;
using Services.Interfaces;

namespace oop_lab3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private IShopService _shopService;

    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpPost]
    public void AddShop([FromBody]ShopDto dto)
    {
        _shopService.AddShop(dto.Code, dto.Name, dto.Address);
    }
    
    [HttpPost("/supply/{code}")]
    public void AddProduct([FromRoute]int code,[FromBody]List<ProvideProductsDto> dto)
    {
        _shopService.AddProductsSupply(code, dto);
    }

    [HttpPost("/mrot/{code}")]
    public IEnumerable<ProvideProductsDto> GetMrot([FromRoute]int code, [FromBody]decimal cash)
    {
        return _shopService.CheckMROT(code, cash);
    }
    
    [HttpPost("/purchase/{code}")]
    public IActionResult MakePurchase([FromRoute]int code, [FromBody]List<PurchaseProductDto> dtos)
    {
        var sum = _shopService.MakePurchase(code, dtos);
        return sum == -1 ? Ok("Данный заказ невозможен, не хватает товаров!") : Ok($"Сумма заказа: {sum}");
    }
    
    /*[HttpPost("/check")]
    public IActionResult CheckMinSum([FromBody]List<PurchaseProductDto> dtos)
    {
        var shop = _shopService.CheckMinSum(dtos);
        return shop == null ? Ok("Невозможно") : Ok($"Сумма заказа: {shop.Sum}");
    }*/
}