using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PERSISTENCE
{
    public class ApplicationDbContext  :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Reads> Reads { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
            builder.Entity<Books>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

        }
    }
}
