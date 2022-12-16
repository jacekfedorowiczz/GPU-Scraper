using GPU_Scraper.Data;
using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares;
using GPU_Scraper.Services;
using GPU_Scraper.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cfg = builder.Configuration;

builder.Services.AddDbContext<GPUScraperDbContext>(options =>
{
    options.UseSqlServer(cfg.GetConnectionString("ScraperDbLocal"));
});

builder.Services.AddAutoMapper(builder.GetType().Assembly);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IGPUScraperService, GPUScraperService>();
builder.Services.AddScoped<XkomScraper>();
builder.Services.AddScoped<MoreleScraper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
