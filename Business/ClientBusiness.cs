using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class ClientBusiness : BaseBusiness<Client>
    {
        public ClientBusiness(ExpressDbContext context) : base(context) { }
        public ClientBusiness() : base() { }
    }
}
