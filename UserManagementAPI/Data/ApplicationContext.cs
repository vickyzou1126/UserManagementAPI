using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;

namespace UserManagementAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Property(u => u.id).ValueGeneratedOnAdd();
            builder.Entity<Account>().Property(u => u.id).ValueGeneratedOnAdd();

            builder.Entity<User>().HasOne(a => a.Account)
                                  .WithOne(b => b.User)
                                  .HasForeignKey<Account>(b => b.userId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
