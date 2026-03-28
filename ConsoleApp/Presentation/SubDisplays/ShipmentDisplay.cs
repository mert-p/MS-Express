using Business;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Presentation.SubDisplays
{
    internal class ShipmentDisplay
    {
        private readonly ShipmentBusiness shipmentBusiness = new ShipmentBusiness();
        private readonly ClientBusiness clientBusiness = new ClientBusiness();
        private readonly CourierBusiness courierBusiness= new CourierBusiness();
        private readonly ShipmentServiceBusiness shipmentServiceBusiness = new ShipmentServiceBusiness();


        private readonly MishoHelper mishoHelper = new MishoHelper();

        private void ShipmentMenu()
        {
            mishoHelper.ShowHeader("Shipment Management");
            Console.WriteLine("1. All Shipments");
            Console.WriteLine("2. Add Shipment");
            Console.WriteLine("3. Update Shipment");
            Console.WriteLine("4. Fetch Shipment by ID");
            Console.WriteLine("5. Delete Shipment");
            Console.WriteLine("0. <-Back");
        }

        public async Task Input()
        {
            int input;
            do
            {
                Console.Clear();
                ShipmentMenu();
                input = mishoHelper.ReadIntInput("Please select an option:");
                switch (input)
                {
                    case 1:
                        await ListAllShipments();
                        break;
                    case 2:
                        await AddShipment();
                        break;
                    case 3:
                        await UpdateShipment();
                        break;
                    case 4:
                        await FetchShipment();
                        break;
                    case 5:
                        await DeleteShipment();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any key..."); Console.ReadKey(); Console.Clear();
            } while (input != 0);
        }

        private async Task ListAllShipments()
        {
            var shipments = await shipmentBusiness.GetAll();
            if (shipments.Count == 0)
            {
                Console.WriteLine("No shipment found.");
                return;
            }
            mishoHelper.ShowHeader("All Shipments");
            foreach (var shipment in shipments)
            {
                Console.WriteLine(shipment);
            }
        }

        private async Task AddShipment()
        {
            bool service=false;
            mishoHelper.ShowHeader($"Creating Shipment");
            Console.WriteLine("1.Without service");
            Console.WriteLine("2.Wit service");
            int input = mishoHelper.ReadIntInput("Please select an option:");
            switch (input)
            {
                case 1:
                    service = false;
                    break;
                case 2:
                    service = true;
                    break;
                default:
                    break;
            }
            Shipment shipment = new Shipment();
            shipment.SenderId = mishoHelper.ReadIntInput("Enter sender ID:");
            shipment.ReceiverId = mishoHelper.ReadIntInput("Enter receiver ID:");
            shipment.CourierId = mishoHelper.ReadIntInput("Enter courier ID:");
            shipment.Weight = mishoHelper.ReadDecimalInput("Enter weight:");
            shipment.Price = mishoHelper.ReadDecimalInput("Enter price:");
            shipment.Type = mishoHelper.ReadStringInput("Enter type:");
            shipment.Date = DateTime.Now.AddDays(10);
            shipment.Status = mishoHelper.ReadStringInput("Enter status:");

            await shipmentBusiness.Add(shipment);
            Console.WriteLine("Shipment added successfully.");
        }

        private async Task UpdateShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to update:");
            var shipment = await shipmentBusiness.GetById(shipmentId);

            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }

            await FetchShipmentById(shipmentId);

            shipment.SenderId = mishoHelper.ReadIntInput("Enter new sender ID:");
            shipment.ReceiverId = mishoHelper.ReadIntInput("Enter new receiver ID:");
            shipment.CourierId = mishoHelper.ReadIntInput("Enter new courier ID:");
            shipment.Weight = mishoHelper.ReadDecimalInput("Enter new weight:");
            shipment.Price = mishoHelper.ReadDecimalInput("Enter new price:");
            shipment.Type = mishoHelper.ReadStringInput("Enter new type:");
            shipment.Status = mishoHelper.ReadStringInput("Enter new status:");

            await shipmentBusiness.Update(shipment);

            Console.WriteLine("Shipment updated successfully.");
        }

        private async Task FetchShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to fetch:");
            await FetchShipmentById(shipmentId);
        }

        private async Task DeleteShipment()
        {
            var shipmentId = mishoHelper.ReadIntInput("Enter Shipment ID to delete:");
            var shipment = await shipmentBusiness.GetById(shipmentId);
            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }
            await shipmentBusiness.Delete(shipmentId);
            Console.WriteLine("Shipment deleted successfully.");
        }

        public async Task FetchShipmentById(int shipmentId)
        {
            var shipment = await shipmentBusiness.GetById(shipmentId);
            if (shipment == null)
            {
                Console.WriteLine("Shipment not found.");
                return;
            }
            Console.WriteLine(shipment);
        }
    }
}
