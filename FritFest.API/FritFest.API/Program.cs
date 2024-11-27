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
builder.Services.AddDbContext<FestivalContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    // Ensure the token validation parameters are configured properly
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}/",
        ValidAudience = builder.Configuration["Auth0:Audience"],
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuth", policy => policy.RequireAuthenticatedUser());
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

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter("RequireAuth"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    // Set up Swagger to use OAuth2 for authentication
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Scheme = "bearer",
        BearerFormat = "jwt",
        Description = "Auth0 JWT Bearer token",
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/authorize"),
                TokenUrl =  new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "read:all", "Read access" },
                    { "write:all", "Write access" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new List<string> { "read:all", "write:all" }
        }
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseRateLimiter();

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get )
    {
        // Allow unauthenticated access for GET /api/Artiests
        await next.Invoke();
    }
    else
    {
        // Require authentication for other routes
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next.Invoke();
    }
});

app.MapControllers();

app.Run();
