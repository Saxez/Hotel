using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Xml;

namespace Project1.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<DifferenceDataHotel> DifferenceDataHotel { get; set; }

        public DbSet<EnteredDataHotel> EnteredDataHotel { get; set; }

        public DbSet<Groups> Groups { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<MassEvent> MassEvents { get; set; }

        public DbSet<RecordDataHotel> RecordDataHotel { get; set; }

        public DbSet<Settler> Settler { get; set; }


        public DbSet<UserXHotel> UserXHotels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=admin");
        }
    }
}
