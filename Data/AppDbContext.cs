using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace WebApplication1.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }


        public DbSet<Article> Articles { get; set; }

        public DbSet<Video> Videoes { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<WorkShop> WorkShops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

