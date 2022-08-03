using Memo.Auth;
using Memo.Auth.Config;
using Memo.Auth.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// База данных
var databaseConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ApplicationContext>(p => 
    p.UseNpgsql(databaseConnection));

// Конфиги
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// Контроллеры
builder.Services.AddControllers();

// Сервисы
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Накатываем миграции на базу
var databaseContext = app.Services.GetRequiredService<ApplicationContext>();
await databaseContext.Database.MigrateAsync();

// Маршрутизация по контроллерам
app.MapControllers();
app.MapGet("/", async context
    => await context.Response.WriteAsync("The authorization service has been launched! Have a cup of coffee and relax..."));

app.Run();