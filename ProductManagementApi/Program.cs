using MassTransit;
using MicroserviceDemo.Contracts.Products;
using MicroserviceDemo.DataBase;
using MicroserviceDemo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x 
    => x.UseSqlite("DataSource=ProductsDb.db"));

builder.Services.AddMassTransit(c =>
{
    var assembly = typeof(Program).Assembly;

    c.AddConsumers(assembly);
    c.AddSagaStateMachines(assembly);
    c.AddSagas(assembly);
    c.AddActivities(assembly);
    
    c.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
