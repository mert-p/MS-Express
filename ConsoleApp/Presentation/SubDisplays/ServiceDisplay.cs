using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class ServiceDisplay
    {
        private readonly ServiceBusiness serviceBusiness = new ServiceBusiness();

        private readonly MishoHelper mishoHelper = new MishoHelper();

        private void ServiceMenu()
        {
            mishoHelper.ShowHeader("Service Management");
            Console.WriteLine("1. All Services");
            Console.WriteLine("2. Add Service");
            Console.WriteLine("3. Update Service");
            Console.WriteLine("4. Fetch Service by ID");
            Console.WriteLine("5. Delete Service");
            Console.WriteLine("0. <-Back");
        }

        public async Task Input()
        {
            int input;
            do
            {
                Console.Clear();
                ServiceMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");

                switch (input)
                {
                    case 1:
                        await ListAllServices();
                        break;
                    case 2:
                        await AddService();
                        break;
                    case 3:
                        await UpdateService();
                        break;
                    case 4:
                        await FetchService();
                        break;
                    case 5:
                        await DeleteService();
                        break;
                }

                Console.WriteLine("Press any key...");
                Console.ReadKey();
                Console.Clear();

            } while (input != 0);
        }

        private async Task ListAllServices()
        {
            var services = await serviceBusiness.GetAllServices();

            if (services.Count == 0)
            {
                Console.WriteLine("No services found.");
                return;
            }

            mishoHelper.ShowHeader("All Services");

            foreach (var service in services)
            {
                Console.WriteLine(service);
            }
        }

        private async Task AddService()
        {
            Service service = new Service();
            service.Name = mishoHelper.ReadStringInput("Enter name:");
            service.Price = mishoHelper.ReadDecimalInput("Enter price:");

            await serviceBusiness.AddService(service);
            Console.WriteLine("Service added successfully.");
        }

        private async Task UpdateService()
        {
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID to update:");
            var service = await serviceBusiness.GetServiceById(serviceId);

            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            await FetchServiceById(serviceId);

            service.Name = mishoHelper.ReadStringInput("Enter new name:");
            service.Price = mishoHelper.ReadDecimalInput("Enter new price:");

            await serviceBusiness.UpdateService(service);
            Console.WriteLine("Service updated successfully.");
        }

        private async Task FetchService()
        {
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID to fetch:");
            await FetchServiceById(serviceId);
        }

        private async Task DeleteService()
        {
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID to delete:");
            var service = await serviceBusiness.GetServiceById(serviceId);

            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            await serviceBusiness.DeleteService(serviceId);
            Console.WriteLine("Service deleted successfully.");
        }

        public async Task FetchServiceById(int serviceId)
        {
            var service = await serviceBusiness.GetServiceById(serviceId);

            if (service == null)
            {
                Console.WriteLine("Service not found.");
                return;
            }

            Console.WriteLine(service);
        }
    }
}
