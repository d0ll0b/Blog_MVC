using Microsoft.EntityFrameworkCore;
using Blog_MVC.Models;
using Blog_MVC.Data;
using Blog_MVC.Middlewares;
using Vite.AspNetCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogContext>(options => {
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BlogContext") ??
        throw new InvalidOperationException("Connection string 'BlogContext' not found.")
    );
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// 加入 Vite 支援
builder.Services.AddViteServices(options =>
{
    options.DevServerPort = 5173; // 必須對應 Vite 開發伺服器的 port
    options.ServerAutoRun = true; // 可選：啟動 ASP.NET Core 時自動啟動 `npm run dev`
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// 使用 Vite Middleware
app.UseViteDevelopmentServer();

//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles(); // 添加靜態文件中間件

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "client", "dist")
    ),
    RequestPath = ""
});

app.MapFallbackToFile("index.html");

app.MapDefaultControllerRoute();

app.Run();

