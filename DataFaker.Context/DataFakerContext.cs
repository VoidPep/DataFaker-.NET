using DataFaker.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace DataFaker.Context;

public interface IDataFakerContext
{
    DbSet<Usuario> Usuarios { get; set; }

    int SaveChanges();
}

public class DataFakerContext(DbContextOptions options) : DbContext(options), IDataFakerContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Path.GetTempPath(), "datafaker.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}