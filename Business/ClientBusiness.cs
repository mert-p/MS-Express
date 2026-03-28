using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.ViewModels;

namespace Business
{
    public class ClientBusiness : BaseBusiness<Client>
    {
        public ClientBusiness(ExpressDbContext context) : base(context) { }
        public ClientBusiness() : base() { }

        public async Task<Client> GetClientByIdWithShipment(int id)
        {
            return await _context.Clients.Include(s => s.SentShipments).ThenInclude(s => s.Courier).Include(s => s.ReceivedShipments).ThenInclude(s => s.Courier).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
