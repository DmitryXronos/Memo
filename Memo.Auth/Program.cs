using Autofac;
using Autofac.Extensions.DependencyInjection;
using Memo.Auth;
using Memo.Auth.Config;
using Memo.Auth.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регаем Autofac как DI
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// База данных
var databaseConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ApplicationContext>(p => 
    p.UseNpgsql(databaseConnection));

// Конфиги
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// Контроллеры
builder.Services.AddControllers();

// Самописные сервисы
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<JwtService>().As<IJwtService>().SingleInstance();
    containerBuilder.RegisterType<PasswordService>().As<IPasswordService>().SingleInstance();
    containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
});

var app = builder.Build();

// Накатываем миграции на базу
using var scope = app.Services.CreateScope();
var databaseContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
await databaseContext.Database.MigrateAsync();

// Маршрутизация по контроллерам
app.MapControllers();
app.MapGet("/", async context
    => await context.Response.WriteAsync("The authorization service has been launched! Have a cup of coffee and relax..."));

app.Run();