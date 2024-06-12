using Carter;
using Catalog.API.Data;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddCarter();

builder.Services.AddDbContext<CategoryContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<BrandContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);
builder.Services.AddDbContext<ProductContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Singleton
    );

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapCarter();
app.UseRouting();
app.MapGet("/", () => "Hello World!");

//using (var scope = app.Services.CreateScope())
//{
//    var brandContext = scope.ServiceProvider.GetRequiredService<BrandContext>();
//    BrandDbInnitializer.InitDb(app);
//}


app.Run();


