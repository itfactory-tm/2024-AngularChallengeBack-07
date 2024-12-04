using FritFest.API;
using FritFest.API.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<FestivalContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<FestivalContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GetAccess", policy => policy.RequireClaim("permissions", "getall:artiests"));
});
builder.Services.AddRateLimiter(
    options =>
    {
        options.AddFixedWindowLimiter("PublicLimiter", config =>
        {
            config.PermitLimit = 100; // Allow 100 requests
            config.Window = TimeSpan.FromMinutes(1); // Per minute
        });
    }
    );

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Swagger services
builder.Services.AddSwaggerService();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://fritfest.com");
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRateLimiter();


app.MapControllers();

// Ensure the database is seeded
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<FestivalContext>();
//    DbInitializer.Initialize(context);
//}


app.Run();
