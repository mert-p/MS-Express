using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class CustomerBusiness : IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public CustomerBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public CustomerBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCustomer(Customer customer)
        {
            var item = await _context.Customers.FindAsync(customer.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            if (_contextOwned)
            {
                _context.Dispose();
            }
        }
    }
}
