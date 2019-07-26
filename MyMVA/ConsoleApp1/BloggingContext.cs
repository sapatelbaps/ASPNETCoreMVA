using System.Collections.Generic;

namespace ConsoleApp1
{
    using System.Drawing;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Post> Posts { get; set; }

        public string _tenantId { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyMVA-ConsoleApp1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theme>().HasData(
            new Theme { ThemeId = 1, Name = "MSDN", TitleColor = Color.AliceBlue.Name },
            new Theme { ThemeId = 2, Name = "TechNet", TitleColor = Color.DarkBlue.Name },
            new Theme { ThemeId = 3, Name = "Personal", TitleColor = Color.LightBlue.Name });

            // Way of telling relationship how we want/have configured.
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne()
                .HasForeignKey(b => b.BlogId);

            //Configure entity filters
            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
        }

        public override int SaveChanges()
        {
            _tenantId = "test";

            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added && e.Metadata.GetProperties().Any(p => p.Name == "TenantId")))
            {
                item.CurrentValues["TenantId"] = _tenantId;
            }

            foreach (var item in ChangeTracker.Entries<Post>()
                .Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChanges();
        }

    }

    public class Theme
    {
        public int ThemeId { get; set; }

        public string Name { get; set; }

        public string TitleColor { get; set; }
    }

    public class Blog
    {
        public int Id { get; set; }

        public string BlogUrl { get; set; }

        public string TenantId { get; set; }

        public Theme Theme { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public bool IsDeleted { get; set; }
    }

}
