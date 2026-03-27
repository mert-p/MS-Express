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

        public async Task<List<ClientViewModel>> GetAllShipmentViews()
        {
            var clients = await GetAll();
            return clients.Select(MapToViewModel).ToList();
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
