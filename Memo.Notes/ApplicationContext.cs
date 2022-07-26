using Memo.Notes.Models;
using Microsoft.EntityFrameworkCore;

namespace Memo.Notes;

/// <summary>Контекст подключения к БД</summary>
public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    /// <summary>Заметки</summary>
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ограничения на длину полей
        modelBuilder.Entity<Note>().Property(p => p.Title).HasMaxLength(100);
        modelBuilder.Entity<Note>().Property(p => p.Text).HasMaxLength(1000);
        // Настраиваем дату создания
        modelBuilder.Entity<Note>().Property(p => p.Date).HasDefaultValueSql("NOW()");
    }
}
