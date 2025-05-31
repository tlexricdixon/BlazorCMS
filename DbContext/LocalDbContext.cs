using CmsModels;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class LocalDbContext : DbContext
{
    public DbSet<PageContent> Pages { get; set; }
    public DbSet<UserProfile> Users { get; set; }
    public DbSet<Posts> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=localcms.db");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Posts>());

        // If you add more SyncEntity-derived models:
        // modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Comment>());
    }
}
