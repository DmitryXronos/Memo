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

// Сервисы
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Аутентификация
app.UseAuthentication();
// Авторизация
app.UseAuthorization();

app.Run();