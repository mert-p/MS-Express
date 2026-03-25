using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class ClientDisplay
    {
        private readonly ClientBusiness clientBusiness = new ClientBusiness();

        private readonly MishoHelper mishoHelper = new MishoHelper();


        private void ClientMenu()
        {
            mishoHelper.ShowHeader("Client Management");
            Console.WriteLine("1. All Clients");
            Console.WriteLine("2. Add Client");
            Console.WriteLine("3. Update Client");
            Console.WriteLine("4. Fetch Client by ID");
            Console.WriteLine("5. Delete Client");
            Console.WriteLine("0. <-Back");
        }

        public async Task Input()
        {
            
            int input;
            do
            {
                Console.Clear();
                ClientMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await ListAllClients();
                        break;
                    case 2:
                        await AddClient();
                        break;
                    case 3:
                        await UpdateClient();
                        break;
                    case 4:
                        await FetchClient();
                        break;
                    case 5:
                        await DeleteClient();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }
        private async Task ListAllClients()
        {
            var clients = await clientBusiness.GetAllClients();
            if (clients.Count == 0)
            {
                Console.WriteLine("No clients found."); 
                return;
            } 
            mishoHelper.ShowHeader("All Clients");
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
        }
        private async Task AddClient()
        {
            Client client = new Client();
            client.FirstName = mishoHelper.ReadStringInput("Enter first name:");
            client.LastName = mishoHelper.ReadStringInput("Enter last name:");
            client.Phone = mishoHelper.ReadPhoneInput("Enter phone number:");
            client.Email = mishoHelper.ReadGmailInput("Enter email:");
            client.Address = mishoHelper.ReadStringInput("Enter address:");
            await clientBusiness.AddClient(client);
            Console.WriteLine("Client added successfully.");
        }
        private async Task UpdateClient()
        {
            var clientId = mishoHelper.ReadIntInput("Enter Client ID to update:"); 
            var client = await clientBusiness.GetClientById(clientId); 
            if (client == null)
            {
                Console.WriteLine("Client not found."); 
                return;
            }
            await FetchClientById(clientId);
            client.FirstName = mishoHelper.ReadStringInput("Enter new first name:");
            client.LastName = mishoHelper.ReadStringInput("Enter new last name:");
            client.Phone = mishoHelper.ReadPhoneInput("Enter new phone number:");
            client.Email = mishoHelper.ReadGmailInput("Enter new email:");
            client.Address = mishoHelper.ReadStringInput("Enter new address:");
            await clientBusiness.UpdateClient(client);

            Console.WriteLine("Client updated successfully.");
        }
        private async Task FetchClient()
        {
            var clientId = mishoHelper.ReadIntInput("Enter Client ID to fetch:");
            await FetchClientById(clientId);
        }
        private async Task DeleteClient()
        {
            var clientId = mishoHelper.ReadIntInput("Enter Client ID to delete:"); 
            var client = await clientBusiness.GetClientById(clientId); 
            if (client == null)
            {
                Console.WriteLine("Client not found."); 
                return;
            }
            await clientBusiness.DeleteClient(clientId); 
            Console.WriteLine("Client deleted successfully.");
        }
        public async Task FetchClientById(int clientId)
        {
            var client = await clientBusiness.GetClientById(clientId);
            if (client == null)
            {
                Console.WriteLine("Client not found.");
                return;
            }
            Console.WriteLine(client);
        }
    }
}
