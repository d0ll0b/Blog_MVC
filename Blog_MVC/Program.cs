using Microsoft.EntityFrameworkCore;
using Blog_MVC.Models;
using Blog_MVC.Data;
using Blog_MVC.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogContext>(options => {
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BlogContext") ??
        throw new InvalidOperationException("Connection string 'BlogContext' not found.")
    );
});

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles(); // 添加靜態文件中間件

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

