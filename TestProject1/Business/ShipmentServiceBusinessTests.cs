using Business;
using Data;
using Data.Models;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace TestProject1.Services
{
    public class ShipmentServiceBusinessTests
    {
        private static ShipmentServiceBusiness CreateService(ExpressDbContext context) => new(context);


        [Fact]
        public async Task GetAllTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                Notes="fasa"
            });

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 2,
                ServiceId = 2,
                ExtraPrice = 10,
                 Notes = "tqqtqt"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetAllShipmentServices();

            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task GetTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                 Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetShipmentServiceByIds(1, 1);

            Assert.NotNull(result);
      
        }

        [Fact]
        public async Task GetTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                 Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetShipmentServiceByIds(1, 1);

           
            Assert.Equal(5, result.ExtraPrice);
        }



        [Fact]
        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                 Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var result = await svc.GetAllShipmentServices();

            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.UpdateShipmentService(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 15,
                Notes = "fasa"
            });

            Assert.Equal(15, ctx.ShipmentServices.First().ExtraPrice);
        }

        [Fact]
        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                ExtraPrice = 5,
                Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.DeleteShipmentService(1, 1);

            Assert.Empty(ctx.ShipmentServices);
        }
    }
}

