using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Controllers;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .AllowAnyMethod()
            //.AllowAnyOrigin()
            //.AllowAnyHeader()
            .WithOrigins("http://localhost",
                                "http://localhost:4200",
                                "http://localhost:3000",
                                "http://localhost:5058",
                                "https://www.centralconnect.ca",
                                "http://20.151.226.58:8080")

                .WithMethods("POST", "PATCH", "PUT", "DELETE", "GET");

            //.AllowAnyMethod()
            //.AllowAnyOrigin()
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseInMemoryDatabase("ProductJsonDatabase"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
