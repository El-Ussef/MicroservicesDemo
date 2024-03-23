using IdentityServiceApi.Context;
using IdentityServiceApi.Contrat;
using IdentityServiceApi.Entities;
using IdentityServiceApi.Helpers;
using IdentityServiceApi.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));


builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=myApp.db"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapIdentityApi<User>();

//app.MapGet("/", () => "Hello World!");

app.MapControllers();
app.Run();