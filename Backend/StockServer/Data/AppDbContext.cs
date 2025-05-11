using Microsoft.EntityFrameworkCore;
using StockServer.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Stock> Stocks { get; set; }
}
