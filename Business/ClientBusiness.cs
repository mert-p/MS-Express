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
        public async Task<ClientViewModel> GetClientViewById(int id)
        {
            var client = await GetById(id);
            return MapToViewModel(client);
        }

        public async Task<List<ClientViewModel>> GetAllClientsViews()
        {
            var clients = await GetAll();
            return clients.Select(MapToViewModel).ToList();
        }
        public async Task<Client> GetClientByIdWithShipment(int id)
        {
            return await _context.Clients.Include(s => s.SentShipments).ThenInclude(s => s.Courier).Include(s => s.ReceivedShipments).ThenInclude(s => s.Courier).FirstOrDefaultAsync(c => c.Id == id);
        }
        private ClientViewModel MapToViewModel(Client s)
        {
            return new ClientViewModel
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}",
                Phone = $"{s.Phone}",
                Email = $"{s.Email}",
                Address = $"{s.Address}",
            };
        }
    }
}
