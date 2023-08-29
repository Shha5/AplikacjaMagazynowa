using AplikacjaMagazynowaAPI.Services;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using DataAccessLibrary.Data;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Helpers;
using DataAccessLibrary.Helpers.Interfaces;
using DataAccessLibrary.SqlAccess;
using DataAccessLibrary.SqlAccess.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IProductData, ProductData>();
builder.Services.AddSingleton<IOrderData, OrderData>();
builder.Services.AddTransient<IDateTimeHelper, DateTimeHelper>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
