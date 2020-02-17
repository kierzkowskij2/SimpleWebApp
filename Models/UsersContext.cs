using Microsoft.EntityFrameworkCore;


namespace SimpleWebApp.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext (DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<User>();
                //.Property(p => p.Id)
                //.HasColumnType("bigint");
            config.ToTable("user");
        }

        public DbSet<User> Users { get; set; }
    }
}
