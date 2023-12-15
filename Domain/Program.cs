using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Realization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IProductService, ProductService>();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();