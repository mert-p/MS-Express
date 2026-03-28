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
          
                Notes="fasa"
            });

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 2,
                ServiceId = 2,
              
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
                 Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetShipmentServiceByIds(1, 1);

           
            Assert.Equal("fasa", result.Notes);
        }



        [Fact]
        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            await svc.AddShipmentService(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                Notes = "fasa"
            });

                 await ctx.SaveChangesAsync();
            Assert.Single(ctx.ShipmentServices);
        }

        [Fact]
        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
                
                Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.UpdateShipmentService(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
               
                Notes = "gaga"
            });

            Assert.Equal("gaga", ctx.ShipmentServices.First().Notes);
        }

        [Fact]
        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.ShipmentServices.Add(new ShipmentService
            {
                ShipmentId = 1,
                ServiceId = 1,
             
                Notes = "fasa"
            });

            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.DeleteShipmentService(1, 1);

            Assert.Empty(ctx.ShipmentServices);
        }
    }
}

