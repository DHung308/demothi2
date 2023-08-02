using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AAAATHIDEMO.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AAAATHIDEMOContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AAAATHIDEMOContext") ?? throw new InvalidOperationException("Connection string 'AAAATHIDEMOContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
