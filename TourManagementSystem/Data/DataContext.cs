using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Models;

namespace TourManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Blog> Blogs { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<BookingDetailsDTO> BookingDetailsDTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookingDetailsDTO>().Ignore(b => b.Service);
        }
    }
}
