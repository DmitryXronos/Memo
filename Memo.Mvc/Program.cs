using Memo.Mvc.Config;
using Memo.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Конфиги
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// Mvc
builder.Services.AddControllersWithViews();

// Сервисы
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICurrentUserInfoService, CurrentUserInfoService>();

var app = builder.Build();

// Сопоставление контроллеров с маршрутами
app.UseRouting();
app.MapDefaultControllerRoute();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("Profile/Index", permanent: false);
        return Task.CompletedTask;
    });
});

app.UseStaticFiles();

app.Run();