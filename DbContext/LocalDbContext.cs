using CmsModels;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class LocalDbContext : DbContext
{
    public DbSet<PageContent> Pages { get; set; }
    public DbSet<UserProfile> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=localcms.db");
}
