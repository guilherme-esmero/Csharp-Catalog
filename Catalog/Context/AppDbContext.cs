using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }
}
