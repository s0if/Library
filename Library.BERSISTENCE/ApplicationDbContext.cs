using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BERSISTENCE
{
    public class ApplicationDbContext  :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Reads> Reads { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Books>()
                .HasOne(b=>b.Publisher)
                .WithOne(p=>p.Book)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Books>()
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
