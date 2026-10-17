using GoMontag.Shared.Entities.Extras;
using GoMontag_Shared.Entities;
using GoMontag_Shared.Entities.Revisions;

namespace GoMontag_WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.CoverImageURL)
                .IsRequired(false); // or true if it should be required

            modelBuilder.Entity<Category>()
                .Property(p => p.Slug)
                .HasConversion<string>(); // Simple conversion if needed

            modelBuilder.Entity<Format>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Formats)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<FormatImage> FormatImages { get; set; }
        public DbSet<FormatRevision> FormatRevisions { get; set; }
        public DbSet<FormatType> FormatTypes { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<NewsBlog> NewsBlogs { get; set; }
    }
}
