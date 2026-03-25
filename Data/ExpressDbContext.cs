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
        public DbSet<Shipment> Shipments { get; set; } 
        public DbSet<ShipmentService> ShipmentServices { get; set; }
        public DbSet<Service> Services { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-38F848J\SQLEXPRESS; Database = ExpressDb; Integrated Security = True; TrustServerCertificate = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Client ↔ Shipment (Sender)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientSender)
                .WithMany(c => c.SentShipments)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Client ↔ Shipment (Receiver)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientReceiver)
                .WithMany(c => c.ReceivedShipments)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Courier ↔ Shipment (One-to-Many)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Courier)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.CourierId)
                .OnDelete(DeleteBehavior.Cascade);

            // ShipmentService (Many-to-Many with payload)
            modelBuilder.Entity<ShipmentService>()
                .HasKey(ss => new { ss.ShipmentId, ss.ServiceId });

            modelBuilder.Entity<ShipmentService>()
                .HasOne(ss => ss.Shipment)
                .WithMany(s => s.ShipmentServices)
                .HasForeignKey(ss => ss.ShipmentId);

            modelBuilder.Entity<ShipmentService>()
                .HasOne(ss => ss.Service)
                .WithMany(s => s.ShipmentServices)
                .HasForeignKey(ss => ss.ServiceId);
        }
    }
}
