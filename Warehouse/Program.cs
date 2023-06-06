using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WarehouseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseContext") ?? throw new InvalidOperationException("Connection string 'WarehouseContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //se agrega para que se muestren los errores de la base de datos, despues de instalar Microsoft.Diagnostics.EntityFrameworkCore

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else //se agrega este ELSE para que se muestren los errores de la base de datos, despues de instalar Microsoft.Diagnostics.EntityFrameworkCore
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

//solo en desarrollo por que borra toda la base de datos.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WarehouseContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
