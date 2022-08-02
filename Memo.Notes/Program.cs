using System.Reflection;
using MediatR;
using Memo.Notes;
using Memo.Notes.Config;
using Memo.Notes.Services;
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

// MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Сервисы
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICurrentUserInfoService, CurrentUserInfoService>();

var app = builder.Build();

// Маршрутизация по контроллерам
app.MapControllers();
app.MapGet("/", async context
    => await context.Response.WriteAsync("The notes service has been launched! Have a cup of coffee and relax..."));

app.Run();