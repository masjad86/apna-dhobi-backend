using Microsoft.EntityFrameworkCore;
namespace ApnaDhobi.Core.Infrastructure.Contexts;

public class CoreDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}