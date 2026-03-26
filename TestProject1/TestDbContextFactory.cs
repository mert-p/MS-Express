using Data;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public static class TestDbContextFactory
    {
        public static ExpressDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ExpressDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ExpressDbContext(options);
        }
    }
}