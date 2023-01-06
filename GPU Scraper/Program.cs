using FluentValidation;
using FluentValidation.AspNetCore;
using GPU_Scraper.Data;
using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares;
using GPU_Scraper.Services;
using GPU_Scraper.Services.Contracts;
using GPUScraper.Authentication;
using GPUScraper.Models.Models;
using GPUScraper.Seeder;
using GPUScraper.Services;
using GPUScraper.Services.Contracts;
using GPUScraper.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cfg = builder.Configuration;
var authenticationSettings = new AuthenticationSettings();

cfg.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});



builder.Services.AddDbContext<GPUScraperDbContext>(options =>
{
    options.UseSqlServer(cfg.GetConnectionString("ScraperDbLocal"));
});

builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<IGPUScraperService, GPUScraperService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<XkomCrawler>();
builder.Services.AddScoped<MoreleCrawler>();
builder.Services.AddScoped<GPUCrawler>();
builder.Services.AddScoped<GPUUpdater>();

builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();

builder.Services.AddScoped<Seeder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var seed = scope.ServiceProvider.GetService<Seeder>();
seed.SeedDatabase();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
