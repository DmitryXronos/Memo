using Memo.Auth.Enums;
using Memo.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Memo.Auth;

/// <summary>Контекст подключения к БД</summary>
public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    
    /// <summary>Пользователи</summary>
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Email и имя пользака уникальны
        modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(p => p.Name).IsUnique();
        // Ограничения на длину полей
        modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(30);
        modelBuilder.Entity<User>().Property(p => p.PasswordHash).HasMaxLength(64);
        // Дефолтная роль пользака
        modelBuilder.Entity<User>().Property(p => p.Role).HasDefaultValue(RoleType.User);
        // Настраиваем дату регистрации
        modelBuilder.Entity<User>().Property(p => p.RegistrationDate).HasDefaultValueSql("NOW()");
    }
}