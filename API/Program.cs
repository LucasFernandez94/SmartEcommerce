using API.Aplication.Mapping;
using API.Aplication.Services;
using API.Domain.Repositories;
using API.Domain.UnitOfWork;
using API.Domain.Validations;
using API.Infrastucture.Data;
using API.Infrastucture.Repository;
using API.Infrastucture.UoW;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped<IDataAccesService, SqlServerService>();

// Add  DbContext
builder.Services.AddDbContext<AppDbContext>(
    config => {
        config.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductosDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    });

// Add Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

//Add Mapping
builder.Services.AddAutoMapper(typeof(ProductMap));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
