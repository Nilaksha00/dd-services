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


app.Run();
