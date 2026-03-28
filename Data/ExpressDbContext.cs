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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server= DESKTOP-GHPFS2U\SQLEXPRESS; Database = ExpressDb; Integrated Security = True; TrustServerCertificate = True;");
            }
        }
        public ExpressDbContext(DbContextOptions<ExpressDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientSender)
                .WithMany(c => c.SentShipments)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ClientReceiver)
                .WithMany(c => c.ReceivedShipments)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Courier)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.CourierId)
                .OnDelete(DeleteBehavior.Cascade);

            
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
