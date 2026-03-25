using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class ShipmentServiceDisplay
    {
        private readonly ShipmentServiceBusiness  shipmentServiceBusiness = new ShipmentServiceBusiness();

        private readonly MishoHelper mishoHelper = new MishoHelper();

        private void ShipmentServiceMenu()
        {
            mishoHelper.ShowHeader("ShipmentService Management");
            Console.WriteLine("1. All ShipmentServices");
            Console.WriteLine("2. Add ShipmentService");
            Console.WriteLine("3. Update ShipmentService");
            Console.WriteLine("4. Fetch ShipmentService by ID");
            Console.WriteLine("5. Delete ShipmentService");
            Console.WriteLine("0. <-Back");
        }

        public async Task Input()
        {
            int input;
            do
            {
                Console.Clear();
                ShipmentServiceMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await ListAllShipmentServices();
                        break;
                    case 2:
                        await AddShipmentService();
                        break;
                    case 3:
                        await UpdateShipmentService();
                        break;
                    case 4:
                        await FetchShipmentService();
                        break;
                    case 5:
                        await DeleteShipmentService();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }

        private async Task ListAllShipmentServices()
        {
            var shipmentServices = await shipmentServiceBusiness.GetAllShipmentServices();
            if (shipmentServices.Count == 0)
            {
                Console.WriteLine("No shipment service found.");
                return;
            }
            mishoHelper.ShowHeader("All ShipmentServices");
            foreach (var shipmentService in shipmentServices)
            {
                Console.WriteLine(shipmentService);
            }
        }

        private async Task AddShipmentService()
        {
            ShipmentService shipmentService = new ShipmentService();
            shipmentService.ShipmentId = mishoHelper.ReadIntInput("Enter shipment ID:");
            shipmentService.ServiceId = mishoHelper.ReadIntInput("Enter service ID:");
            shipmentService.ExtraPrice = mishoHelper.ReadDecimalInput("Enter extra price:");
            shipmentService.Notes = mishoHelper.ReadStringInput("Enter notes:");

            await shipmentServiceBusiness.AddShipmentService(shipmentService);
            Console.WriteLine("ShipmentService added successfully.");
        }

        private async Task UpdateShipmentService()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to update:");
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID to update:");

            var shipmentService = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentId, serviceId);

            if (shipmentService == null)
            {
                Console.WriteLine("ShipmentService not found.");
                return;
            }

            await FetchShipmentServiceById(shipmentId, serviceId);

            shipmentService.ExtraPrice = mishoHelper.ReadDecimalInput("Enter new extra price:");
            shipmentService.Notes = mishoHelper.ReadStringInput("Enter new notes:");

            await shipmentServiceBusiness.UpdateShipmentService(shipmentService);

            Console.WriteLine("ShipmentService updated successfully.");
        }

        private async Task FetchShipmentService()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID:");
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID:");
            await FetchShipmentServiceById(shipmentId, serviceId);
        }

        private async Task DeleteShipmentService()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to delete:");
            var serviceId = mishoHelper.ReadIntInput("Enter Service ID to delete:");

            var shipmentService = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentId, serviceId);

            if (shipmentService == null)
            {
                Console.WriteLine("ShipmentService not found.");
                return;
            }

            await shipmentServiceBusiness.DeleteShipmentService(shipmentId, serviceId);
            Console.WriteLine("ShipmentService deleted successfully.");
        }

        public async Task FetchShipmentServiceById(int shipmentId, int serviceId)
        {
            var shipmentService = await shipmentServiceBusiness.GetShipmentServiceByIds(shipmentId, serviceId);

            if (shipmentService == null)
            {
                Console.WriteLine("ShipmentService not found.");
                return;
            }

            Console.WriteLine(shipmentService);
        }
    }
}