using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Text;
using Tests;

namespace TestProject1.Services
{
    public class ClientBusinessTests
    {
        private static ClientBusiness CreateService(ExpressDbContext context) => new(context);



        [Fact]

        public async Task GetAllTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]

        public async Task GetTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.NotNull(result);

            Assert.Equal("Client2", result.FirstName);

        }

        [Fact]

        public async Task GetTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);


            Assert.Equal(2, result.Id);

        }

        [Fact]

        public async Task GetTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.Equal("Client2", result.FirstName);

        }

        [Fact]
        public async Task AddClient_ShouldInsertClient()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            await svc.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });

            Assert.Single(ctx.Clients);
        }

        [Fact]
        public async Task GetClientById_ShouldReturnClient_WhenExists()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            var result = await svc.GetById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientById_ShouldReturnNull_WhenMissing()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            var result = await svc.GetById(999);

            Assert.Null(result);
        }


        [Fact]
        public async Task UpdateClient_ShouldChangeFirstName()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Update(new Client { Id = 1, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });

            Assert.Equal("Client2", ctx.Clients.First().FirstName);
        }


        [Fact]
        public async Task Update_ShouldNotCrash_WhenEntityMissing()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            await svc.Update(new Client { Id = 999 });

            Assert.Empty(ctx.Shipments);
        }

     


        [Fact]
        public async Task GetAll_ShouldReturnEmpty_WhenNoData()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            var result = await svc.GetAll();

            Assert.Empty(result);
        }


        [Fact]
        public async Task Delete_ShouldNotCrash_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            await svc.Delete(999);

            Assert.Empty(ctx.Shipments);
        }


        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotFound()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            var result = await svc.GetById(999);

            Assert.Null(result);
        }



        [Fact]

        public async Task GetWithShipmentTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            //Mert bashe tuk
            var result = await svc.GetById(2);

            Assert.Equal(2, result.Id);
  
        }



        [Fact]

        public async Task GetWithShipmentTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            //Mert bashe tuk
            var result = await svc.GetById(2);

            Assert.Equal("Client2", result.FirstName);
 
                
        }


        [Fact]

        public async Task GetWithShipmentTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            //Mert bashe tuk
            var result = await svc.GetById(2);

            
            Assert.NotNull(result.ReceivedShipments);
        }


        [Fact]

        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            await svc.Add(new Client { FirstName = "Client1", LastName = "Test", Email = "abcdefg@gmail.com", Address = "Bolgrovo", Phone = "08028114129" });
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);

            Assert.Equal(1, result1.Count);


        }

        [Fact]

        public async Task AddTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            await svc.Add(new Client { FirstName = "Client1", LastName = "Test", Email = "abcdefg@gmail.com", Address = "Bolgrovo", Phone = "08028114129" });
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);


            Assert.NotNull(result2);


        }

        [Fact]

        public async Task AddTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            await svc.Add(new Client { FirstName = "Client1", LastName = "Test", Email = "abcdefg@gmail.com", Address = "Bolgrovo", Phone = "08028114129" });
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);

            Assert.Equal("Client1", result2.FirstName);

        }

        [Fact]

        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Update(new Client { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Email = "toshkoafrikanski@gmail.com", Address = "Sofia", Phone = "0899999999" });



            Assert.Equal("Toshko", ctx.Clients.First().FirstName);




        }

        [Fact]

        public async Task UpdateTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Update(new Client { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Email = "toshkoafrikanski@gmail.com", Address = "Sofia", Phone = "0899999999" });




            Assert.NotEmpty(ctx.Clients.First().LastName);



        }

        [Fact]


        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Delete(1);


            Assert.Equal(0, ctx.Clients.Count());

        }

        [Fact]


        public async Task DeleteTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Delete(1);



            Assert.Null(ctx.Clients.FirstOrDefault());

        }

        [Fact]
        public async Task DeleteClient_ShouldRemoveClient()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();

            var svc = CreateService(ctx);

            await svc.Delete(1);

            Assert.Empty(ctx.Clients);
        }

        [Fact]
        public async Task DeleteClient_ShouldNotCrash_WhenMissing()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);

            await svc.Delete(999);

            Assert.Empty(ctx.Clients);
        }

        [Fact]

        public async Task GetClientByIDWithShipmentTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 1, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });

            await svc.GetClientByIdWithShipment(1);

            Assert.NotNull(ctx.Clients.First().SentShipments);
        }
    }
}
