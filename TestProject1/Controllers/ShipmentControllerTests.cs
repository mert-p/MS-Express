using Business;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace TestProject1.Controllers
{
    public class ShipmentControllerTests
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
    }
}
