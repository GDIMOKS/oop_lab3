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
    
    [HttpPost("/supply/{id}")]
    public void AddProduct([FromRoute]int id,[FromBody]List<ProvideProductsDto> dto)
    {
        _shopService.AddProductsSupply(id, dto);
    }
}