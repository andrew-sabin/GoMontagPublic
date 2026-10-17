using GoMontag_Shared.Entities.Logs;
using GoMontag_Shared.Entities.Recommendations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoMontag_WebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<RecommendationItem> Recommendations { get; set; }
        public DbSet<RecommendationList> RecommendationLists { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecommendationList>()
                .HasMany(r => r.Items)
                .WithOne(i => i.RecommendationList)
                .HasForeignKey("RecommendationListId");

            modelBuilder.Entity<RecommendationItem>()
                .Property(i => i.ProductId);

            // Exclude Identity entities that don't need to be mapped
            modelBuilder.Ignore<IdentityPasskeyData>();
        }
    }

     
}
