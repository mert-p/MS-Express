using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class ClientBusiness : IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public ClientBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public ClientBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }
        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }
        public async Task<Client> GetClientByIdWithShipment(int id)
        {
            return await _context.Clients.Include(s=>s.SentShipments).ThenInclude(s=>s.Courier).Include(s => s.ReceivedShipments).ThenInclude(s => s.Courier).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddClient(Client customer)
        {
            await _context.Clients.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateClient(Client customer)
        {
            var item = await _context.Clients.FindAsync(customer.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteClient(int id)
        {
            var customer = await _context.Clients.FindAsync(id);
            if (customer != null)
            {
                _context.Clients.Remove(customer);
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
