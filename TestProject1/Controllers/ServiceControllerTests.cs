using Business;
using Data;
using Data.Models;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace TestProject1.Controllers
{
    public class ServiceControllerTests
    {
        private static ServiceBusiness CreateService(ExpressDbContext context) => new(context);

        [Fact]
        public async Task GetAllTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            ctx.Services.Add(new Service { Id = 2, Name = "Service2", Price = 20.00m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            ctx.Services.Add(new Service { Id = 2, Name = "Service2", Price = 20.00m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.NotNull(result);
            Assert.Equal("Service2", result.Name);
        }

        [Fact]
        public async Task GetByIdTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            ctx.Services.Add(new Service { Id = 2, Name = "Service2", Price = 20.00m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetById(2);


            Assert.Equal("Service2", result.Name);
        }

        [Fact]
        public async Task GetByIdTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            ctx.Services.Add(new Service { Id = 2, Name = "Service2", Price = 20.00m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.Equal(2, result.Id);
        }



        [Fact]
        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var result = await svc.GetAll();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task AddTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var result = await svc.GetById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var result = await svc.GetById(1);

            Assert.Equal("Service1", result.Name);
        }

        [Fact]
        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Update(new Service { Id = 1, Name = "UpdatedService", Price = 99.99m });

            Assert.Equal("UpdatedService", ctx.Services.First().Name);
        }

        [Fact]
        public async Task UpdateTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Update(new Service { Id = 1, Name = "UpdatedService", Price = 99.99m });

            Assert.True(ctx.Services.First().Price > 0);
        }

        [Fact]
        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Delete(1);

            Assert.Equal(0, ctx.Services.Count());
        }

        [Fact]
        public async Task DeleteTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Services.Add(new Service { Id = 1, Name = "Service1", Price = 10.50m });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Delete(1);

            Assert.Null(ctx.Services.FirstOrDefault());
        }
    }
}

