using MediatR.Sample.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Sample.Core.Persistence
{
    public class SamplesContext : DbContext
    {
        public SamplesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>(cfg =>
            {
                cfg.HasKey(p => p.Id);

                cfg.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
