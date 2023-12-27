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
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<StockService>();
builder.Services.AddSingleton<OutletService>();

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

app.MapGet("/api/products/{id}", async (ProductService productService, string id) =>
{
    var product = await productService.GetProductById(id);
    return product;
});

app.MapDelete("/api/products/{id}", async (ProductService productService, string id) =>
{
    await productService.DeleteProductById(id);
    return Results.Ok();
});

app.MapPut("/api/products/{id}", async (ProductService productService, string id, Product updatedProduct) =>
{
    await productService.UpdateProductById(id, updatedProduct);
    return Results.Ok();
});

// Add Order API Routes
app.MapGet("/api/orders", async (OrderService orderService) =>
{
    var orders = await orderService.GetOrderList();
    return orders;
});

app.MapPost("/api/orders/add", async (OrderService orderService, Order order) =>
{
    await orderService.CreateOrder(order);
    return Results.Ok();
});

app.MapPut("/api/orders/{id}", async (OrderService orderService, String id, Order order) =>
{
    await orderService.UpdateOrder(id, order);
    return Results.Ok();
});

app.MapGet("/api/orders/{id}", async (OrderService orderService, string id) =>
{
    var order = await orderService.GetOrderById(id);
    return order;
});

app.MapDelete("/api/orders/{id}", async (OrderService orderService, string id) =>
{
    await orderService.DeleteOrderById(id);
    return Results.Ok();
});

// Add Stock API Routes
app.MapGet("/api/stock/{outletId}", async (StockService stockService, string outletId) =>
{
    var stockList = await stockService.GetStockListByOutletId(outletId);
    return stockList;
});

app.MapPost("/api/stock/{outletID}", async (StockService stockService, Stock newStock) =>
{
    await stockService.CreateStock(newStock);
    return Results.Ok();
});

app.MapPut("/api/stock/{outletId}/{productId}/{newStockLevel}", async (StockService stockService, string outletId, string productId, int newStockLevel) =>
{
    await stockService.UpdateStock(outletId, productId, newStockLevel);
    return Results.Ok();
});

// Add Outlet API Routes
app.MapGet("/api/outlets", async (OutletService outletService) =>
{
    var outlets = await outletService.GetOutletList();
    return outlets;
});

app.MapGet("/api/outlets/{outletId}", async (OutletService outletService, string outletId) =>
{
    var outlet = await outletService.GetOutletById(outletId);
    return outlet;
});


app.Run();
