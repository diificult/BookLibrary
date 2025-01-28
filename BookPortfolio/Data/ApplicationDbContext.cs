using BookPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.BookId }));
            builder.Entity<Portfolio>().HasOne(u => u.AppUser).WithMany(u => u.Portfolios).HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>().HasOne(u => u.Book).WithMany(u => u.Portfolios).HasForeignKey(p => p.BookId);



            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
