using System.Reflection;
using System.Runtime.Intrinsics.X86;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.Contracts.IRepositories;
using PurchaseManagementApi.DataBase;
using PurchaseManagementApi.ExternalServices;
using PurchaseManagementApi.ExternalServices.Contracts;
using PurchaseManagementApi.Heplers;
using PurchaseManagementApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();        
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddHttpClient();
builder.Services.Configure<ProductApiOptions>(builder.Configuration.GetSection("ExternalApi:ProductApi"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
        // cfg.Host("localhost", h =>
        // {
        //     // h.ConfigureBatchPublish(x =>
        //     // {
        //     //     x.Enabled = true;
        //     //     x.Timeout = TimeSpan.FromMilliseconds(2);
        //     // });
        //     
        // });
    });
});

builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IProductsApi, ProductsApi>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using ( var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}