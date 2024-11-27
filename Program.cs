using System.Text;
using CarRentalSystem.Data;
using CarRentalSystem.Repositories;
using CarRentalSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CarRentalSystem.Filters;

var builder = WebApplication.CreateBuilder(args);

// Get JWT parameters from .env
var jwtKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");


// Load envionment variables from .env file
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
EnvironmentService.Load(dotenv);

builder.Services.AddAuthentication(i =>
{
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(i =>
{
    i.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
    };
});

// Authorization
builder.Services.AddAuthorization(i =>
{
    i.AddPolicy("AdminOnly", j => j.RequireRole("Admin"));
    i.AddPolicy("All", j => j.RequireRole("Admin", "User"));
});


// Add services to the container.

builder.Services.AddScoped<TransactionLogService>();
builder.Services.AddControllers(c => c.Filters.Add<TransactionLogAttribute>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT service
builder.Services.AddSingleton<JWTService>();

// Add User repository and service
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Cars repository and service
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICarsService, CarsService>();

// Add CarRental repository and service
builder.Services.AddScoped<ICarRentalRepository, CarRentalRepository>();
builder.Services.AddScoped<ICarRentalService, CarRentalService>();

// Add TransactionLog service

// Add database context for SQL Server
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
