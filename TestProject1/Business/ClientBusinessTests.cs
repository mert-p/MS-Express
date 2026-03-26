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
            var result = await svc.GetAllClients();

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
            var result = await svc.GetClientById(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Client2", result.FirstName);

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
            var result = await svc.GetClientByIdWithShipment(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Client2", result.FirstName);
            Assert.NotNull(result.ReceivedShipments);
        }


        [Fact]

        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            await svc.AddClient(new Client { FirstName = "Client1", LastName = "Test", Email = "abcdefg@gmail.com", Address = "Bolgrovo", Phone = "08028114129" });
            var result1 = await svc.GetAllClients();
            var result2 = await svc.GetClientById(1);

            Assert.Equal(1, result1.Count);
            Assert.NotNull(result2);
            Assert.Equal("Client1", result2.FirstName);

        }

        [Fact]

        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.UpdateClient(new Client { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Email = "toshkoafrikanski@gmail.com", Address = "Sofia", Phone = "0899999999" });


            Assert.Equal(1, ctx.Clients.Count());
            Assert.Equal("Toshko", ctx.Clients.First().FirstName);
            Assert.NotEmpty(ctx.Clients.First().LastName);



        }

        [Fact]


        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo", Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.DeleteClient(1);

            Assert.Empty(ctx.Clients);
            Assert.Equal(0, ctx.Clients.Count());
            Assert.Null(ctx.Clients.FirstOrDefault());
        }
    }
}
