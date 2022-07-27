using Memo.Mvc.Interfaces;
using Memo.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Аутентификация по куки
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    {
        // Путь, по которому перенаправляются анонимные пользователи
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Login");
    });

// Авторизация
builder.Services.AddAuthorization();

// Mvc
builder.Services.AddControllersWithViews();

// Сервисы
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Сопоставление контроллеров с маршрутами
app.MapDefaultControllerRoute();

// Аутентификация
app.UseAuthentication();
// Авторизация
app.UseAuthorization();

app.Run();