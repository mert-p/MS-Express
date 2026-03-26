using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
namespace Tests
{
    public class ClientBusinessTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ExpressDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ExpressDbContext(options))
            {
                context.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Email = "abcdefg@gmai.com", Address = "Bolqrovo",Phone = "0802814129" });
                context.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Email = "gfedcba@gmail.com", Address = "Hiroshima", Phone = "0841941921" });
                context.SaveChanges();

                ClientBusiness ClientBusiness = new ClientBusiness(context);

                // Act
                List<Client> Clients = await ClientBusiness.GetAll();

                // Assert
                
            }
        }

        //[Fact]
        //public async Task GetTest()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<LibraryDbContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new LibraryDbContext(options))
        //    {
        //        context.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" });
        //        context.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Biography = "gfedcba", DateOfBirth = DateTime.Now.AddYears(-2), ImageUrl = "randomUrl" });
        //        context.SaveChanges();

        //        ClientBusiness ClientBusiness = new ClientBusiness(context);

        //        // Act
        //        Client Client = await ClientBusiness.GetAsync(2);

        //        // Assert
        //        Assert.That(Client, Is.Not.Null);
        //        Assert.That(Client.Id, Is.EqualTo(2));
        //        Assert.That(Client, Is.EqualTo(context.Clients.Find(2)));
        //    }
        //}

        //[Test]
        //public async Task GetWithIncludesTest()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<LibraryDbContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new LibraryDbContext(options))
        //    {
        //        context.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" });
        //        context.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Biography = "gfedcba", DateOfBirth = DateTime.Now.AddYears(-2), ImageUrl = "randomUrl" });
        //        context.SaveChanges();

        //        ClientBusiness ClientBusiness = new ClientBusiness(context);

        //        // Act
        //        Client Client = await ClientBusiness.GetWithIncludesAsync(2);

        //        // Assert
        //        Assert.That(Client, Is.Not.Null);
        //        Assert.That(Client.Id, Is.EqualTo(2));
        //        Assert.That(Client, Is.EqualTo(context.Clients.Find(2)));
        //        Assert.That(Client.Books, Is.Not.Null); // Navigation property expected to be loaded
        //    }
        //}

        //[Test]
        //public async Task AddTest()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<LibraryDbContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new LibraryDbContext(options))
        //    {
        //        ClientBusiness ClientBusiness = new ClientBusiness(context);
        //        Client Client = new Client { Id = 3, FirstName = "Client", LastName = "3", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" };

        //        // Act
        //        await ClientBusiness.AddAsync(Client);

        //        // Assert
        //        Assert.That(Client, Is.Not.Null);
        //        Assert.That(Client.Id, Is.EqualTo(3));
        //        Assert.That(Client, Is.EqualTo(context.Clients.Find(3)));
        //    }
        //}

        //[Test]
        //public async Task UpdateTest()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<LibraryDbContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new LibraryDbContext(options))
        //    {
        //        context.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" });
        //        context.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Biography = "gfedcba", DateOfBirth = DateTime.Now.AddYears(-2), ImageUrl = "randomUrl" });
        //        context.SaveChanges();

        //        ClientBusiness ClientBusiness = new ClientBusiness(context);
        //        Client Client = new Client { Id = 1, FirstName = "UpdatedClient", LastName = "1", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" };

        //        // Act
        //        await ClientBusiness.UpdateAsync(Client);

        //        // Assert
        //        Assert.That(Client, Is.Not.Null);
        //        Assert.That(context.Clients.Count(), Is.EqualTo(2));
        //        Assert.That(Client.Id, Is.EqualTo(1));
        //        Assert.That(Client.FirstName, Is.EqualTo("UpdatedClient"));
        //    }
        //}

        //[Test]
        //public async Task DeleteTest()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<LibraryDbContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var context = new LibraryDbContext(options))
        //    {
        //        context.Clients.Add(new Client { Id = 1, FirstName = "Client", LastName = "1", Biography = "abcdefg", DateOfBirth = DateTime.Now.AddYears(-1), ImageUrl = "randomUrl" });
        //        context.Clients.Add(new Client { Id = 2, FirstName = "Client", LastName = "2", Biography = "gfedcba", DateOfBirth = DateTime.Now.AddYears(-2), ImageUrl = "randomUrl" });
        //        context.SaveChanges();

        //        ClientBusiness ClientBusiness = new ClientBusiness(context);

        //        // Act
        //        await ClientBusiness.DeleteAsync(2);

        //        // Assert
        //        Assert.That(context.Clients.Count(), Is.EqualTo(1));
        //        Assert.That(context.Clients.Find(2), Is.Null);
        //    }
        //}
    }
}
