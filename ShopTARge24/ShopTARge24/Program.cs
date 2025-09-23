using ShopTARge24.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddDbContext<ShopTARge24Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseStaticFiles(new StaticFileOptions
{
   FileProvider = new PhysicalFileProvider(
       Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
   RequestPath = "/multipleFileUpload"
});

var uploadPath = Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload");
if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();