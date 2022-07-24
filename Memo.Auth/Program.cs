using Memo.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// База данных
var databaseConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ApplicationContext>(p => 
    p.UseNpgsql(databaseConnection));

// Контроллеры
builder.Services.AddControllers();

var app = builder.Build();

// Маршрутизация по контроллерам
app.MapControllers();
app.MapGet("/", async context
    => await context.Response.WriteAsync("The authorization service has been launched! Have a cup of coffee and relax..."));

app.Run();