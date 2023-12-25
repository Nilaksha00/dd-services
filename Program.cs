using DDServices;
using DDServices.Models;
using DDServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => "Base API Endpoint");

app.MapPost("/api/login", async (Login login, AuthService authService) =>
{

    if (string.IsNullOrEmpty(login.email) || string.IsNullOrEmpty(login.password))
    {
        return null;
    }

    var user = await authService.LoginUser(login);

    if (user == null)
    {
        return null;
    }

    return user;
});

app.MapPost("/api/register", async ( User user, AuthService authService) =>
{
    await authService.CreateUser(user);
    return Results.Ok();
});


app.MapGet("/api/products", async (ProductService productService) =>
{
    var products = await productService.GetProductList();
    return products;
});

app.MapPost("/api/products/add", async (ProductService productService, Product product) =>
{
    await productService.CreateProduct(product);
    return Results.Ok();
});

app.Run();
