using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Data
{
    public class DeliveryTrackingContext :DbContext
    {
        public DeliveryTrackingContext() : base()
        { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ShipmentService> ShipmentServices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-GHPFS2U\SQLEXPRESS; Database = DeliveryTrakingDB; Integrated Security = True; TrustServerCertificate = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Email).HasMaxLength(100);
                entity.Property(c => c.Address).HasMaxLength(200);
            });

         
            modelBuilder.Entity<Courier>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Salary).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Price).HasColumnType("decimal(18,2)");
            });

      
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Type).HasMaxLength(50);
                entity.Property(s => s.Status).HasMaxLength(50);
                entity.Property(s => s.Price).HasColumnType("decimal(18,2)");

              
                entity.HasOne(s => s.Sender)
                    .WithMany(c => c.SentShipments)
                    .HasForeignKey(s => s.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Receiver)
                    .WithMany(c => c.ReceivedShipments)
                    .HasForeignKey(s => s.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

               
                entity.HasOne(s => s.Courier)
                    .WithMany(c => c.Shipments)
                    .HasForeignKey(s => s.CourierId);
            });

            
            modelBuilder.Entity<ShipmentService>(entity =>
            {
                entity.ToTable("ShipmentServices");

                entity.HasKey(ss => new { ss.ShipmentId, ss.ServiceId });

                entity.Property(ss => ss.ExtraPrice)
                      .HasColumnType("decimal(18,2)");

                entity.Property(ss => ss.Notes)
                      .HasMaxLength(500);

                entity.HasOne(ss => ss.Shipment)
                    .WithMany(s => s.ShipmentServices)
                    .HasForeignKey(ss => ss.ShipmentId);

                entity.HasOne(ss => ss.Service)
                    .WithMany(s => s.ShipmentServices)
                    .HasForeignKey(ss => ss.ServiceId);
            }); 
        }
    }
}
