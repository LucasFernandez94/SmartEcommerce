using UI.Service.DTO;
using UI.Service.Interface;
using UI.Service.Service.Customer;
using UI.Service.Service.Order;
using UI.Service.Service.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();


//Http service
builder.Services.AddHttpClient<IService<CustomerDTO>, CustomerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7081/Customer/");
});

builder.Services.AddHttpClient<IService<ProductDTO>, ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/Product/");
});

builder.Services.AddHttpClient<IService<OrderDTO>, OrderService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7281/Order/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
