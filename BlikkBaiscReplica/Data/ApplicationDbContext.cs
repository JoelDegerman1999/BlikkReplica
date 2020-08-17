using BlikkBasicReplica.Models;
using BlikkBasicReplica.Webhooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlikkBasicReplica.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Contacts)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey(x => x.ApplicationUserId);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey(x => x.ApplicationUserId);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
    }
}