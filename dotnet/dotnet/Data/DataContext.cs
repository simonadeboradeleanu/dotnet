using Microsoft.EntityFrameworkCore;
using proiectelul.Models;
using dotnet.Models;

namespace dotnet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet <Shark> Sharks { get; set; }
        public DbSet <Ocean> Oceans { get; set; }
        public DbSet <User> Users { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<UserShark> UserSharks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*//din ContextConfiguration aplicand UserConfiguration
            //modelBuilder.AddConfiguration();
            //fluent api*/

            //one to many
            modelBuilder.Entity<Shark>()
                .HasOne(s => s.Ocean)
                .WithMany(o => o.Sharks)
                .HasForeignKey(s => s.OceanId);

            //many to many
            modelBuilder.Entity<UserShark>()
                .HasKey(us => new { us.UserId, us.SharkId });

            modelBuilder.Entity<UserShark>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSharks)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserShark>()
                .HasOne(us => us.Shark)
                .WithMany(s => s.UserSharks)
                .HasForeignKey(us => us.SharkId);

            //one to one
            modelBuilder.Entity<Shark>()
               .HasOne(s => s.Detail)
               .WithOne(d => d.Shark)
               .HasForeignKey<Detail>(d => d.SharkId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
