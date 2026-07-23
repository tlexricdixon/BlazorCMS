using CmsModels;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class LocalDbContext : DbContext
{
    public DbSet<AnalyticsEntry> AnalyticsEntries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ContactFormSubmission> ContactForm { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<UserProfile> Users { get; set; }
    public DbSet<PageBlock> PageBlocks => Set<PageBlock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new PostTagConfiguration());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Category>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Comment>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Page>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Tag>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<UserProfile>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Settings>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<ContactFormSubmission>());
        modelBuilder.ApplyConfiguration(new PageBlockConfiguration());

        // If you add more SyncEntity-derived models:
        // modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Comment>());
    }
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<SyncEntity>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.LastModified = DateTime.UtcNow;
        }
        return base.SaveChanges();
    }
}
