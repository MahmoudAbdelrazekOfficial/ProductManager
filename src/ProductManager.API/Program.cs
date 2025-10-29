using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductManager.Application.Features.Products.Commands;
using ProductManager.Domain.Interfaces;
using ProductManager.Infrastructure.Data;
using ProductManager.Infrastructure.Repositories;
using ProductManager.Infrastructure.UnitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add controllers & JSON
services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null; 
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductManager API",
        Version = "v1",
        Description = "Clean Architecture + CQRS + FluentValidation + MediatR example + Some Of Concept for Memory Management " // thats for me to remember what is this project about
    });
});


// DbContext with resiliency , timeout , performance optimization
services.AddDbContextPool<MyAppDbContext>(opt =>
    opt.UseSqlServer(
        config.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(60);          // Increase timeout for heavy queries , Timeout in seconds (default 30)
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,                              // Retry up to 3 times
                maxRetryDelay: TimeSpan.FromSeconds(5),  // wait max 10s between retries
                errorNumbersToAdd: null                       // Retry on transient SQL errors
            );
        }
    )
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Improve read performance 
);

// Infrastructure DI
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//services.AddScoped<IRepository<Product>, Repository<Product>>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IProductRepository, ProductRepository>();

// Application DI - MediatR & FluentValidation
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

// Build
var app = builder.Build();

// Middleware
app.UseMiddleware<ProductManager.API.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
