using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Data.Seeders;
using api.Services;
using api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

const string FrontendCorsPolicy = "FrontendCorsPolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000",
                "http://localhost:3001",
                "http://127.0.0.1:3001"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

if (args.Length > 0)
{
    var command = args[0].ToLowerInvariant();
    if (command is "seed" or "reseed")
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (command == "reseed")
        {
            await SeederRunner.ReseedAsync(db);
            Console.WriteLine("Reseed completed.");
        }
        else
        {
            await SeederRunner.SeedAsync(db);
            Console.WriteLine("Seed completed.");
        }

        return;
    }
}

app.UseCors(FrontendCorsPolicy);
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
