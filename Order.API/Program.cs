using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Order.Aplication.Mapping;
using Order.Aplication.Services;
using Order.Domain.Services;
using Order.Domain.UnitOfWork;
using Order.Domain.Validations;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;
using Order.Infrastructure.UoW;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Http Client
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICustomerService, CustomerService>();


// Add  DbContext
builder.Services.AddDbContext<OrderDbContext>(
    config => {
        config.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OrderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    });

// Add Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

//Add Mapping
builder.Services.AddAutoMapper(typeof(OrderMap));

//Serilog
// Agregar configuración desde appsettings.json
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

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
