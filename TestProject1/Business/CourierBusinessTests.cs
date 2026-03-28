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
using Xunit.Sdk;

namespace TestProject1.Services
{
    public class CourierBusinessTests
    {
        private static CourierBusiness CreateService(ExpressDbContext context) => new(context);



        [Fact]

        public async Task GetAllTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]

        public async Task GetTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.NotNull(result);

            Assert.Equal("Courier2", result.FirstName);

        }

        [Fact]

        public async Task GetTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);


            Assert.Equal(2, result.Id);

        }



        [Fact]

        public async Task GetTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var result = await svc.GetById(2);

            Assert.Equal("Courier2", result.FirstName);

        }

        [Fact]

        public async Task GetWithShipmentTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
           
            var result = await svc.GetById(2);

            Assert.Equal(2, result.Id);

        }



        [Fact]

        public async Task GetWithShipmentTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
     
            var result = await svc.GetById(2);

            Assert.Equal("Courier2", result.FirstName);


        }


        [Fact]

        public async Task GetWithShipmentTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Couriers.Add(new Courier { Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
          
            var result = await svc.GetById(2);


            Assert.NotNull(result.Phone);
        }


        [Fact]

        public async Task AddTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);

            Assert.Equal(1, result1.Count);


        }

        [Fact]

        public async Task AddTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);


            Assert.NotNull(result2);


        }

        [Fact]

        public async Task AddTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();

            var svc = CreateService(ctx);
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            var result1 = await svc.GetAll();
            var result2 = await svc.GetById(1);

            Assert.Equal("Courier1", result2.FirstName);

        }

        [Fact]

        public async Task UpdateTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Update(new Courier { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Salary = 1067.67m, Available = false, Phone = "05235623523" });



            Assert.Equal("Toshko", ctx.Couriers.First().FirstName);




        }

        [Fact]

        public async Task UpdateTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Update(new Courier { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Salary = 1067.67m, Available = false, Phone = "05235623523" });



            Assert.NotEmpty(ctx.Couriers.First().LastName);



        }


        [Fact]

        public async Task UpdateTest3()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Update(new Courier { Id = 1, FirstName = "Toshko", LastName = "Afrikanski", Salary = 1067.67m, Available = false, Phone = "05235623523" });



            Assert.False(ctx.Couriers.First().Available);



        }

        [Fact]


        public async Task DeleteTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Delete(1);


            Assert.Equal(0, ctx.Couriers.Count());

        }

        [Fact]


        public async Task DeleteTest2()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            await svc.Delete(1);



            Assert.Null(ctx.Couriers.FirstOrDefault());

        }

        [Fact]
        public async Task GetAvailableCouriersTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();


            var svc = CreateService(ctx);
            List<Courier> available = new List<Courier>()
            {
                 new Courier
                     {
                        Id = 1,
                        FirstName = "Courier1",
                        LastName = "1",
                        Salary = 1060.50m,
                        Available = true,
                        Phone = "0802814129"
                     },
                 new Courier
                     {
                        Id = 3,
                        FirstName = "Courier3",
                        LastName = "3",
                        Salary = 1070.00m,
                        Available = true,
                        Phone = "0851234567"
                     },

                 new Courier
                 {
                     Id = 4,
                     FirstName = "Courier4",
                     LastName = "4",
                     Salary = 1080.00m,
                     Available = true,
                     Phone = "0861234567"

                 }

            };
            List<Courier> NotAvailable = new List<Courier>()
            {
                new Courier
                {
                    Id = 2, FirstName = "Courier2", LastName = "2", Salary = 1067.67m, Available = false, Phone = "0841941921" },
                new Courier
                {
                    Id = 5,
                    FirstName = "Courier5",
                    LastName = "5",
                    Salary = 1090.00m,
                    Available = false,
                    Phone = "0871234567"
                },
                new Courier
                    {
                        Id = 6,
                        FirstName = "Courier6",
                        LastName = "6",
                        Salary = 1100.00m,
                        Available = false,
                        Phone = "0881234567"
                    }


            };

            ctx.Couriers.AddRange(available);
            await ctx.SaveChangesAsync();
            Assert.Equal(available, await svc.GetAvailableCouriers());

        }

        [Fact]

        public async Task GetCouriersByMostSalaryTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);
            List<Courier> couriers = new List<Courier>()
            {
                 new Courier
                     {
                        Id = 1,
                        FirstName = "Courier1",
                        LastName = "1",
                        Salary = 1060.50m,
                        Available = true,
                        Phone = "0802814129"
                     },
                 new Courier
                     {
                        Id =2,
                        FirstName = "Courier2",
                        LastName = "2",
                        Salary = 1070.00m,
                        Available = true,
                        Phone = "0851234567"
                     },
                 new Courier
                 {
                     Id = 3,
                     FirstName = "Courier3",
                     LastName = "3",
                     Salary = 1080.00m,
                     Available = true,
                     Phone = "0861234567"
                 }

            };
            ctx.Couriers.AddRange(couriers);
            await ctx.SaveChangesAsync();  
            Assert.Equal(couriers.OrderByDescending(c => c.Salary).ToList(), await svc.GetCouriersByMostSalary());
        }

        [Fact]
        public async Task GetCouriersByMostLeastTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            var svc = CreateService(ctx);
            List<Courier> couriers = new List<Courier>()
            {
                 new Courier
                     {
                        Id = 1,
                        FirstName = "Courier1",
                        LastName = "1",
                        Salary = 1060.50m,
                        Available = true,
                        Phone = "0802814129"
                     },
                 new Courier
                     {
                        Id =2,
                        FirstName = "Courier2",
                        LastName = "2",
                        Salary = 1070.00m,
                        Available = true,
                        Phone = "0851234567"
                     },
                 new Courier
                 {
                     Id = 3,
                     FirstName = "Courier3",
                     LastName = "3",
                     Salary = 1080.00m,
                     Available = true,
                     Phone = "0861234567"
                 }

            };
            ctx.Couriers.AddRange(couriers);
            await ctx.SaveChangesAsync();
            Assert.Equal(couriers.OrderBy(c => c.Salary).ToList(), await svc.GetCouriersByLeastSalary());
        }

        [Fact]
        public async Task GetCourierByIdWithShipmentsTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
            ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            ctx.Shipments.Add(new Shipment { Id = 2, SenderId = 2, ReceiverId = 1, CourierId = 1, Status = "Delivered", Weight = 2.50m, Price = 20.00m, Type = "Medium" }); 
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            Assert.Equal(2, (await svc.GetCourierByIdWithShipments(1)).Shipments.Count);
        }


        [Fact]
        public async Task GetCourierByIdWithShipmentsAndClientsTest()
        {
            using var ctx = TestDbContextFactory.CreateContext();
           ctx.Couriers.Add(new Courier { Id = 1, FirstName = "Courier1", LastName = "1", Salary = 1060.50m, Available = true, Phone = "0802814129" });
            ctx.Clients.Add(new Client { Id = 1, FirstName = "Client1", LastName = "1", Phone = "0802814129" ,Address="jlkjdals",Email="fkhsgafa@gmail.com"});
            ctx.Clients.Add(new Client { Id = 2, FirstName = "Client2", LastName = "2", Phone = "0841941921" , Address = "khfas", Email = "fsdfdsaa@gmail.com" });
            ctx.Shipments.Add(new Shipment { Id = 1, SenderId = 1, ReceiverId = 2, CourierId = 1, Status = "In Transit", Weight = 1.30m, Price = 10.50m, Type = "Light" });
            ctx.Shipments.Add(new Shipment { Id = 2, SenderId = 2, ReceiverId = 1, CourierId = 1, Status = "Delivered", Weight = 2.50m, Price = 20.00m, Type = "Medium" });
            await ctx.SaveChangesAsync();
            var svc = CreateService(ctx);
            var courierWithDetails = await svc.GetCourierByIdWithShipmentsAndClients(1);
            Assert.Equal(2, courierWithDetails.Shipments.Count);
            Assert.Equal("Client1", courierWithDetails.Shipments.First().ClientSender.FirstName);
            Assert.Equal("Client2", courierWithDetails.Shipments.First().ClientReceiver.FirstName);
        }
    }
}
