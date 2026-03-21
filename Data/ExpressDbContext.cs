using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ExpressDbContext:DbContext
    {
        public ExpressDbContext() : base()
        { }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Shipment> Shipment { get; set; } 
        public DbSet<ShipmentService> ShipmentService { get; set; }
        public DbSet<Service> Services { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-38F848J\SQLEXPRESS; Database = ExpressDb; Integrated Security = True; TrustServerCertificate = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // ShipmentService (JOIN TABLE)
            // =========================
            modelBuilder.Entity<ShipmentService>()
                .HasKey(ss => new { ss.ShipmentId, ss.ServiceId });

            modelBuilder.Entity<ShipmentService>()
                .HasOne(ss => ss.Shipment)
                .WithMany(s => s.ShipmentServices)
                .HasForeignKey(ss => ss.ShipmentId);

            modelBuilder.Entity<ShipmentService>()
                .HasOne(ss => ss.Service)
                .WithMany() // Service nav is private → ignored
                .HasForeignKey(ss => ss.ServiceId);

            // =========================
            // Shipment → Courier
            // =========================
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Courier)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.CourierId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Shipment → Sender (Client)
            // =========================
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientSender)
                .WithMany(c => c.SentShipments)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Shipment → Receiver (Client)
            // =========================
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientReceiver)
                .WithMany(c => c.ReceivedShipments)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
