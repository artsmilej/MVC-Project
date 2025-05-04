using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using AdAgencyWebApp.Data;
using AdAgencyWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// ������ ������ MVC (���������� � ���������������)
builder.Services.AddControllersWithViews();

// ������ AppDbContext (���� ����� MSSQL ��� LocalDB)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������ ���� (��� ���������� ����� �����������)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ������ HttpContextAccessor ��� ������� �� ��� � _Layout.cshtml
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ������������ ������� HTTP-������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ������ ������������ ���

app.UseAuthorization();

// ������� �� �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Users.Any(u => u.Role == "Admin"))
    {
        db.Users.Add(new User
        {
            Email = "admin@site.com",
            Password = "123456",
            Role = "Admin"
        });
        db.SaveChanges();
    }
}