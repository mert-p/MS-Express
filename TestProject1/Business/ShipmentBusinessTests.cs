using Business;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace TestProject1.Services
{
    public class ShipmentBusinessTests
    {
        private static ShipmentBusiness CreateService(ExpressDbContext context) => new(context);

        [Fact]
        public async Task GetAllTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            ctx.Shipments.Add(new Shipment
            {
                Id = 2,
                SenderId = 2,
                ReceiverId = 3,
                CourierId = 2,
                Weight = 2.5m,
                Price = 20,
                Type = "Heavy",
                Date = DateTime.Now,
                Status = "Delivered"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetAll();

            Assert.Equal(2, result.Count);
        }



        [Fact]
        public async Task GetByIdTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetById(1);

            Assert.NotNull(result);

        }



        [Fact]
        public async Task GetByIdTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetById(1);


            Assert.Equal("Pending", result.Status);
        }

        [Fact]
        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            await ctx.SaveChangesAsync();

            var result = await svc.GetAll();

            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Update(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 5,
                Price = 50,
                Type = "Heavy",
                Date = DateTime.Now,
                Status = "Delivered"
            });

            Assert.Equal("Delivered", ctx.Shipments.First().Status);
        }

        [Fact]
        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Delete(1);

            Assert.Empty(ctx.Shipments);
        }

        [Fact]

        public async Task AddWithIdTests()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.AddWithId(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });



            Assert.Equal(1, result);
        }

        [Fact]

        public async Task GetAllShipmentsTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "fgafafafa@gmail.com", Address = "Chernoochene", Phone = "53215628" });
            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });

            var svc = CreateService(ctx);
            var result = svc.GetAllShipments();



            Assert.Equal(1, result.Id);

        }

        [Fact]

        public async Task GetShipmentsWirhServiceTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Services.Add(new Service { Id = 1, Name = "Express", Price = 15.67m });
          
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "fgafafafa@gmail.com", Address = "Chernoochene", Phone = "53215628" });
            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });
             ctx.ShipmentServices.Add(new ShipmentService { ShipmentId = 1, ServiceId = 1, Notes="gaagas" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetShipmentsWirhService();
            Assert.Equal("Express", result[0].ShipmentServices[0].Service.Name);

        }

        [Fact]

        public async Task GetShipmentWirhServiceTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Services.Add(new Service { Id = 1, Name = "Express", Price = 15.67m });

            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "fgafafafa@gmail.com", Address = "Chernoochene", Phone = "53215628" });
            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });
            ctx.ShipmentServices.Add(new ShipmentService { ShipmentId = 1, ServiceId = 1, Notes = "gaagas" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            Shipment result = await svc.GetShipmentWirhService(1);
            Assert.Equal(1,result.Id);

        }
        [Fact]

        public async Task GetShipmentsByStatus()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Shipments.Add(new Shipment
            {
                Id = 1,
                SenderId = 1,
                ReceiverId = 2,
                CourierId = 1,
                Weight = 1.2m,
                Price = 10,
                Type = "Light",
                Date = DateTime.Now,
                Status = "Pending"
            });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetShipmentsByStatus("Pending"); 
            Assert.NotEmpty(result);
            Assert.All(result, s => Assert.Equal("Pending", s.Status));
        }
    }
}
